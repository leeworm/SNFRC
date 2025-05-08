using Unity.VisualScripting;
using UnityEngine;

public class JH_Entity : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }

    #endregion

    [Header("기본정보")]
    public int MaxHP = 100;
    public int CurrentHP;
    public float MoveSpeed = 5f;
    public float JumpForce = 5f;
    public Transform POS;

    [Header("거리정보")]
    [SerializeField] protected Transform GroundCheck;
    [SerializeField] protected float GroundCheckDistance;
    [SerializeField] protected Transform EnemyCheck;
    [SerializeField] protected float EnemyCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    protected float jumpGracePeriodTimer = 0f; // protected로 변경하여 자식 클래스에서 원한다면 접근 가능 (보통은 public 함수 통해 제어)
    public float jumpGraceDuration = 0.15f;  // Inspector에서 조절 가능하도록 public으로 변경

    public int facingDir { get; private set; } = 1;

    protected virtual void Awake()
    {
       
    }

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (jumpGracePeriodTimer > 0)
        {
            jumpGracePeriodTimer -= Time.deltaTime;
        }
    }

    protected virtual void Exit()
    {

    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }

    #region 충돌
    public virtual bool IsGroundDetected()
    {
        // --- 점프 유예 기간 체크 로직 추가 ---
        if (jumpGracePeriodTimer > 0)
        {
            return false; // 유예 기간 중에는 항상 공중으로 판정
        }
        // --- 추가 끝 ---
        if (GroundCheck == null) return false; // GroundCheck Transform이 할당되지 않은 경우 오류 방지
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, GroundCheckDistance, whatIsGround);
    }
    public virtual bool IsEnemyDetected() => Physics2D.Raycast(EnemyCheck.position, Vector2.right * facingDir, EnemyCheckDistance, whatIsGround);


    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(GroundCheck.position, new Vector3(GroundCheck.position.x, GroundCheck.position.y - GroundCheckDistance));
        Gizmos.DrawLine(EnemyCheck.position, new Vector3(EnemyCheck.position.x + EnemyCheckDistance, EnemyCheck.position.y));
    }
    #endregion

    public void InitiateJumpGracePeriod()
    {
        jumpGracePeriodTimer = jumpGraceDuration;
    }
}

