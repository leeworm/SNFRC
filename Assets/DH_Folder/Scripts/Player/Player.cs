using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack Detail")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lasttimeAttacked;
    [SerializeField] protected LayerMask whatIsEnemy;

    public bool isBusy { get; private set; }
    [Header("Movement info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDir { get; private set; } // 대시 방향 (1: 오른쪽, -1: 왼쪽)

    public int currentJumpCount;
    public int maxJumpCount = 2;

    [HideInInspector] public BoxCollider2D col;
    [HideInInspector] public Vector2 originalColliderSize;
    [HideInInspector] public Vector2 originalColliderOffset;


    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerLandState landState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public DashCommandDetector dashCommandDetector { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        // 상태 머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();
        dashCommandDetector = new DashCommandDetector();

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        landState = new PlayerLandState(this, stateMachine, "Land");
        dashState = new PlayerDashState(this, stateMachine, "Dash", dashDir);
        deadState = new PlayerDeadState(this, stateMachine, "Die");        
    }
    protected override void Start()
    {
        base.Start();
        currentJumpCount = maxJumpCount;
        stateMachine.Initialize(idleState);

        col = GetComponent<BoxCollider2D>();
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public Transform GetNearestEnemy()
    {
        float range = 5f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }
    public override void FlipController(float _x)
    {
        // 백스텝 상태에서는 Flip 막기
        if (stateMachine.currentState is PlayerBackstepState)
            return;

        base.FlipController(_x); // 나머지는 기존 방식 유지
    }

}
