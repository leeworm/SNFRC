using Unity.VisualScripting; // 이 using 구문은 그대로 둡니다.
using UnityEngine;
using System; // System.Action 이벤트를 사용하기 위해 필요합니다.

// IDamageable 인터페이스와 BodyPart 열거형을 클래스 외부에 두는 것이 일반적이지만,
// 현재 구조를 유지하기 위해 JH_Entity 클래스 내부에 두겠습니다.
// 만약 다른 스크립트(예: JH_Hitbox)에서 이들을 참조하려면 JH_Entity.IDamageable, JH_Entity.BodyPart 와 같이 접근해야 합니다.

public class JH_Entity : MonoBehaviour, JH_Entity.IDamageable // <--- 변경점: 클래스 내부에 정의된 IDamageable 인터페이스를 구현하도록 명시
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    [Header("기본정보")]
    public int MaxHP = 100;
    public int CurrentHP; // Start에서 MaxHP로 초기화
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;
    public Transform POS;
    public GameObject Missile;

    [Header("거리정보")]
    [SerializeField] protected Transform GroundCheck;
    [SerializeField] protected float GroundCheckDistance;
    [SerializeField] protected Transform EnemyCheck;
    [SerializeField] protected float EnemyCheckDistance;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected LayerMask whatIsGround; // EnemyCheck에도 whatIsGround가 사용되고 있습니다. 적으로 감지할 다른 LayerMask가 필요할 수 있습니다.



    protected float jumpGracePeriodTimer = 0f;
    public float jumpGraceDuration = 0.15f;

    public int facingDir { get; private set; } = 1;

    // 인터페이스와 열거형을 클래스 내부에 정의 (기존 코드 유지)
    // 다른 스크립트에서 참조 시: JH_Entity.IDamageable, JH_Entity.BodyPart
    public interface IDamageable
    {
        void TakeDamage(float amount, BodyPart hitPart);
    }

    public enum BodyPart
    {
        Head,
        Body, // 기존 'Torso' 대신 'Body' 사용 (제공해주신 코드 기준)
        Leg   // 기존 'Legs' 대신 'Leg' 사용 (제공해주신 코드 기준)
    }

    // --- HP 및 데미지 관련 이벤트 및 상태 변수 ---
    public event Action<int, int> OnHealthChanged;  // (현재 체력, 최대 체력)
    public event Action OnKnockced; 

    public bool isKnocked = false;

    protected virtual void Awake()
    {
       
    }

    protected virtual void Start()
    {
        // 자신의 컴포넌트 찾기
        if (sr == null) sr = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        // 스프라이트 렌더러가 없다면 자식에서 찾기
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            Debug.Log($"{gameObject.name}: 자식에서 SpriteRenderer 찾음 - {(sr != null ? "성공" : "실패")}");
        }

        CurrentHP = MaxHP;
        isKnocked = false;
        OnHealthChanged?.Invoke(CurrentHP, MaxHP);
    }

    protected virtual void Update()
    {
        // 게임 매니저가 없거나 일시정지 상태면 움직임 정지
        if (JH_GameManager.Instance == null || JH_GameManager.IsPaused())
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0;
            }
            return;
        }

        // 승리 애니메이션 중이 아닐 때만 일반 업데이트 실행
        if (!JH_GameManager.Instance.IsVictoryAnimationPlaying())
        {
            if (rb != null)
            {
                rb.gravityScale = 1;
            }

            if (isKnocked) return;

            if (jumpGracePeriodTimer > 0)
            {
                jumpGracePeriodTimer -= Time.deltaTime;
            }

            UpdateEnemyDetection();
        }
    }

    protected virtual void Exit() 
    {
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (JH_GameManager.IsPaused()) return;

        if (isKnocked) return; // KO 상태에서는 속도 변경 불가
        if (rb != null) // Null 체크 추가
        {
            rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
            UpdateEnemyDetection();
        }
    }

    #region 충돌 관련 메소드
    public virtual bool IsGroundDetected()
    {
        // 점프 유예 기간 중에는 땅 감지를 어떻게 할지 명확히 해야 합니다.
        // 현재 로직: 유예 기간 중에는 항상 공중으로 판정.
        if (jumpGracePeriodTimer > 0)
        {
            return false;
        }

        if (GroundCheck == null) return false;
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckDistance, whatIsGround);
    }

    public virtual void UpdateEnemyDetection()
    {
        if (EnemyCheck == null) return;

        // 왼쪽 방향 체크
        RaycastHit2D leftHit = Physics2D.Raycast(EnemyCheck.position, Vector2.left, EnemyCheckDistance, enemyLayer);

        // 오른쪽 방향 체크
        RaycastHit2D rightHit = Physics2D.Raycast(EnemyCheck.position, Vector2.right, EnemyCheckDistance, enemyLayer);

        // 디버그 로그
        if (leftHit.collider != null)
            Debug.Log($"{gameObject.name}: 왼쪽에서 적 감지 - {leftHit.collider.name}");
        if (rightHit.collider != null)
            Debug.Log($"{gameObject.name}: 오른쪽에서 적 감지 - {rightHit.collider.name}");

        // 양쪽 중 더 가까운 적을 향해 방향 설정
        if (leftHit.collider != null && rightHit.collider != null)
        {
            // 더 가까운 쪽으로 방향 설정
            float leftDistance = leftHit.distance;
            float rightDistance = rightHit.distance;
            SetFacingDirection(leftDistance < rightDistance ? -1 : 1);
        }
        else if (leftHit.collider != null)
        {
            SetFacingDirection(-1);
        }
        else if (rightHit.collider != null)
        {
            SetFacingDirection(1);
        }
    }

    public virtual bool IsEnemyDetected()
    {
        if (EnemyCheck == null) return false;

        // 현재 바라보는 방향으로만 체크
        return Physics2D.Raycast(EnemyCheck.position, Vector2.right * facingDir, EnemyCheckDistance, enemyLayer);
    }

    protected virtual void OnDrawGizmos()
    {
        if (GroundCheck != null)
            Gizmos.DrawLine(GroundCheck.position, new Vector3(GroundCheck.position.x, GroundCheck.position.y - GroundCheckDistance));

        if (EnemyCheck != null)
        {
            // 왼쪽 방향 레이
            Gizmos.color = Color.red;
            Gizmos.DrawLine(EnemyCheck.position, EnemyCheck.position + Vector3.left * EnemyCheckDistance);

            // 오른쪽 방향 레이
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(EnemyCheck.position, EnemyCheck.position + Vector3.right * EnemyCheckDistance);
        }
    }
    #endregion

    public void InitiateJumpGracePeriod()
    {
        jumpGracePeriodTimer = jumpGraceDuration;
    }

    public virtual void TakeDamage(float amount, BodyPart hitPart)
    {
        if (isKnocked) return; // KO 상태면 데미지 안 받음

        CurrentHP -= Mathf.RoundToInt(amount); // float 데미지를 int로 변환하여 적용
        if (CurrentHP < 0) CurrentHP = 0;

        Debug.Log(gameObject.name + " took " + amount + " damage to " + hitPart + ". Current HP: " + CurrentHP + "/" + MaxHP);
        OnHealthChanged?.Invoke(CurrentHP, MaxHP); // 체력 변경 이벤트 호출

        if (CurrentHP <= 0)
        {
            KO(); // 체력이 0 이하면 KO 처리
        }
        else
        {
            PlayHitReaction(hitPart); // 살아있으면 피격 반응 처리
        }
    }

    protected virtual void PlayHitReaction(BodyPart hitPart)
    {
        if (animator == null)
        {
            Debug.LogError($"{gameObject.name}에 Animator가 없습니다!");
            return;
        }

        Debug.Log(gameObject.name + " playing hit reaction for " + hitPart);
        string triggerName = ""; // Animator Trigger 이름 초기화

        switch (hitPart)
        {
            case BodyPart.Head:
                triggerName = "HeadHitTrigger";
                break;
            case BodyPart.Body: 
                triggerName = "BodyHitTrigger"; 
                break;
            case BodyPart.Leg:  
                triggerName = "LegHitTrigger";  
                break;
               
        }

        if (!string.IsNullOrEmpty(triggerName)) // 유효한 트리거 이름이 있을 때만 실행
        {
            animator.SetTrigger(triggerName);
        }
    }

    protected virtual void KO() 
    {
        if (isKnocked) return; // 이미 KO 처리되었으면 중복 실행 방지

        isKnocked = true; // KO 상태로 변경
        Debug.Log(gameObject.name + " has been knocked out."); // 로그 메시지 변경
        OnKnockced?.Invoke(); // KO 이벤트 호출

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // 움직임 정지
            rb.bodyType = RigidbodyType2D.Static; // 물리적 상호작용 비활성화
        }

        if (animator != null)
        {
            animator.SetTrigger("KOTrigger"); // Animator에 "KOTrigger" 필요
        }

        // 추가적인 KO 처리 (예: 몇 초 후 비활성화, 게임 매니저에 알림 등)
    }

    public void SetFacingDirection(int direction)
    {
        if (direction == 1 || direction == -1)
        {
            facingDir = direction;

            // 회전 적용 (0도 또는 180도)
            transform.rotation = Quaternion.Euler(0, facingDir == -1 ? 180f : 0f, 0);
        }
    }
}