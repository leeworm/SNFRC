using System.Collections;
using UnityEngine;

public class KH_Player : KH_Entity
{
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
    
    #region States
    public KH_PlayerStateMachine stateMachine { get; private set; }

    public KH_PlayerIdleState idleState { get; private set; }
    public KH_PlayerMoveState moveState { get; private set; }
    public KH_PlayerJumpState jumpState { get; private set; }
    public KH_PlayerFallState fallState { get; private set; }
    public KH_PlayerHitState hitState { get; private set; }
    public KH_PlayerShotState shotState { get; private set; }
    public KH_PlayerSetPipeState setPipeState { get; private set; }

    public KH_PlayerHangState hangState { get; private set; }
    public KH_PlayerCutMoveState cutMoveState { get; private set; }
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

        hangState = new KH_PlayerHangState(this, stateMachine, "Hang");
        cutMoveState = new KH_PlayerCutMoveState(this, stateMachine, "Move");
    }

    protected override void Start()
    {
        base.Start();
        sr = GetComponentInChildren<SpriteRenderer>();

        jumpAttackCollider.SetActive(false); // 점프 공격 콜라이더 비활성화

        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

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
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canHit)
        {
            stateMachine.ChangeState(hitState);
            KH_HealthManager.Instance.TakeDamage(1);
        }
    }

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
}
