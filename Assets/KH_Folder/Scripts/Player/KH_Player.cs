using System.Collections;
using System.Threading;
using UnityEngine;

public class KH_Player : KH_Entity
{
    private CapsuleCollider2D capsuleCollider2D;
    public Transform pipeChek;
    public float pipeCheckDistance = 0.1f;
    public LayerMask whatIsPipe;

    private UsePipe usePipe;

    [Header("이동 정보")]
    public float moveSpeed = 12f;
    public float jumpForce;
    public float bouncePower = 10f; // 튕겨나가는 힘

    public GameObject jumpAttackCollider;

    private SpriteRenderer sr;
    public bool canHit = true; // 무적

    [Header("파이어볼 정보")]
    public GameObject FireballPrefab; // 파이어볼 프리팹
    public Transform FireballSpawnPoint; // 파이어볼 발사 위치
    
    [Header("아이템 정보")]
    public GameObject MushRoomPrefab;
    public float mushRoomCoolTime = 10f;
    public float mushRoomTimer = 0; // 버섯 지속시간
    public GameObject SonicPrefab;
    public GameObject Sonic;
    
    [Header("파이프 정보")]
    public GameObject SetPipePrefab;
    [SerializeField] private GameObject UsePipePrefab;
    public float setPipeCoolTime = 15f;
    public float setPipeTimer = 0;
    [Header("에러 조각 정보")]
    public float errorPieceCoolTime = 20f; // 상수
    public float errorPieceTimer = 0;
    public bool isErrorState = false;
    public bool isPlayerRaindow = false;
    public float errorStateDuration = 10f; // 상수
    
    [Header("에러 파이프 정보")]
    public int pipeCount = 1;
    public float pipInOutSpeed = 10f;
    public bool isTelepot = false;
    public bool isTelepotSuccess = false;
    

    #region States
    public KH_PlayerStateMachine stateMachine { get; private set; }

    public KH_PlayerIdleState idleState { get; private set; }
    public KH_PlayerMoveState moveState { get; private set; }
    public KH_PlayerJumpState jumpState { get; private set; }
    public KH_PlayerFallState fallState { get; private set; }
    public KH_PlayerHitState hitState { get; private set; }
    public KH_PlayerShotState shotState { get; private set; }
    public KH_PlayerSetPipeState setPipeState { get; private set; }
    public KH_PlayerInPipeState inPipeState { get; private set; }

    public KH_PlayerHangState hangState { get; private set; }
    public KH_PlayerCutMovingState cutMovingState { get; private set; }
    #endregion

    [SerializeField]public bool isStage1 = true;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new KH_PlayerStateMachine();

        idleState = new KH_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new KH_PlayerMoveState(this, stateMachine, "Move");
        jumpState = new KH_PlayerJumpState(this, stateMachine, "Jump");
        fallState = new KH_PlayerFallState(this, stateMachine, "Jump");
        hitState = new KH_PlayerHitState(this, stateMachine, "Idle");
        shotState = new KH_PlayerShotState(this, stateMachine, "Shot");
        setPipeState = new KH_PlayerSetPipeState(this, stateMachine, "Idle");
        inPipeState = new KH_PlayerInPipeState(this, stateMachine, "Idle");

        hangState = new KH_PlayerHangState(this, stateMachine, "Hang");
        cutMovingState = new KH_PlayerCutMovingState(this, stateMachine, "Move");

    }

    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        jumpAttackCollider.SetActive(false); // 점프 공격 콜라이더 비활성화

        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        mushRoomTimer -= Time.deltaTime;
        setPipeTimer -= Time.deltaTime;
        errorPieceTimer -= Time.deltaTime;

        if(isErrorState)
            errorStateDuration -= Time.deltaTime;

        // 레인 보우 코루틴 스타트
        if(isPlayerRaindow)
        {
            pipeCount = 2;

            errorStateDuration = 10f;
            StartCoroutine(Rainbow());
            isPlayerRaindow = false;
        }

        // 오류 상태 끝
        if(errorStateDuration <= 0 && isErrorState)
        {
            Debug.Log("Stop Rainbow");
            StopCoroutine(Rainbow());
            isErrorState = false;
        }
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && canHit)
        {
            if(collision.gameObject.transform.position.x > transform.position.x) // 적이 오른쪽에 있을 때
            {
                rb.AddForce(new Vector2(-bouncePower, 0), ForceMode2D.Impulse); // 왼쪽으로 튕기기
            }
            else if(collision.gameObject.transform.position.x < transform.position.x) // 적이 왼쪽에 있을 때
            {
                rb.AddForce(new Vector2(bouncePower, 0), ForceMode2D.Impulse); // 오른쪽으로 튕기기
            }

            stateMachine.ChangeState(hitState);
            KH_HealthManager.Instance.TakeDamage(1);
        }

        if (collision.gameObject.name == "item_mushroom(Clone)")
        {
            Debug.Log("버섯 먹음");
            KH_SoundManager.Instance.PlaySFXSound("marioMushroom");
            KH_HealthManager.Instance.Heal(2);

            Destroy(collision.gameObject); // 버섯 먹으면 삭제
        }
        if (collision.gameObject.name == "item_sonic(Clone)")
        {
            Debug.Log("소닉 먹음");
            KH_SoundManager.Instance.PlaySFXSound("marioSonic");

            Instantiate(Sonic, transform.position, Quaternion.identity);

            Destroy(collision.gameObject); // 소닉 먹으면 삭제
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canHit)
        {
            stateMachine.ChangeState(hitState);
            KH_HealthManager.Instance.TakeDamage(1);
        }

        if (collision.gameObject.CompareTag("EnemyBullet") && canHit)
        {
            stateMachine.ChangeState(hitState);
            KH_HealthManager.Instance.TakeDamage(1);
        }
    }

    //public bool IsPipeDetected() => Physics2D.Raycast(pipeChek.position, Vector2.down, pipeCheckDistance, whatIsPipe);
    public bool IsPipeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(pipeChek.position, Vector2.down, pipeCheckDistance, whatIsPipe);

        if (hit.collider != null)
        {
            usePipe = hit.collider.GetComponentInParent<UsePipe>();
            Debug.Log(usePipe);

            return true;
        }

        return false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawLine(pipeChek.position, new Vector3(pipeChek.position.x, pipeChek.position.y - pipeCheckDistance));
    }
    #endregion

    public IEnumerator Flicker()
    {
        for (int i = 0; i < 10; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Bounce()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, bouncePower); // 위로 튕기기
    }

    public void Hang()
    {
        stateMachine.ChangeState(hangState);
    }

    //

    public void CallMushRoom()
    {   
        KH_SoundManager.Instance.PlaySFXSound("marioItemDown");

        GameObject Prefab;
        if(isErrorState)
            Prefab = SonicPrefab;
        else
            Prefab = MushRoomPrefab;

        if(KH_GameManager.Instance.koopa.phaseState == PhaseState.Phase2)
        {
            Instantiate(Prefab, new Vector2(transform.position.x, -190), Quaternion.identity);
            return;
        }
        Instantiate(Prefab, new Vector2(transform.position.x, 8), Quaternion.identity);
    }

    public void SetPipe()
    {
        GameObject pipe = Instantiate(SetPipePrefab, transform.position, Quaternion.identity);
    }

    IEnumerator Rainbow()
    {
        while(true)
        {
            if(errorStateDuration <= 0)
            {
                sr.color = new Color(255,255,255);
                yield break;
            }

            sr.color = new Color(255,0,0);
            yield return new WaitForSeconds(0.05f);
            sr.color = new Color(0,255,0);
            yield return new WaitForSeconds(0.05f);
            sr.color = new Color(0,0,255);
            yield return new WaitForSeconds(0.05f);
        }
    }

    //

    // 충돌 파이프 번호 가져오기
    public int GetCollisionPipeNumber()
    {
        return usePipe.pipeNumber;
    }
    
    public void PlayerIsTrigger(bool _trigger)
    {
        capsuleCollider2D.isTrigger = _trigger;
    }

    public void InTelepotPipe(Vector3 _targetVec)
    {
        if(transform.position == _targetVec)
        {
            Debug.Log("Telepot !");
            Telepot();
            return;
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, _targetVec, pipInOutSpeed * Time.deltaTime);
    }

    public void Telepot()
    {
        if(GetCollisionPipeNumber() == 0)
        {
            transform.position = KH_GameManager.Instance.telepotPipe[1].vec;
        }
        else if(GetCollisionPipeNumber() == 1)
        {
            transform.position = KH_GameManager.Instance.telepotPipe[0].vec;
        }

        isTelepot = true;
    }

    public void OutTelepotPipe(Vector3 _targetVec)
    {
        if(transform.position == _targetVec)
        {
            KH_SoundManager.Instance.PlaySFXSound("marioPipe");
            Debug.Log("Telepot Success !");
            isTelepotSuccess = true;
            return;
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, _targetVec, pipInOutSpeed * Time.deltaTime);
    }
}
