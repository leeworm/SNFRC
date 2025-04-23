using UnityEngine;

public class KH_Player : KH_Entity
{
    [Header("이동 정보")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("적 총돌 정보")]
    [SerializeField] public Transform enemyChek;
    [SerializeField] public float enemyCheckDistance = 0.2f;
    [SerializeField] public LayerMask whatIsEnemy;
    
    #region States
    public KH_PlayerStateMachine stateMachine { get; private set; }

    public KH_PlayerIdleState idleState { get; private set; }
    public KH_PlayerMoveState moveState { get; private set; }
    public KH_PlayerJumpState jumpState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new KH_PlayerStateMachine();

        idleState = new KH_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new KH_PlayerMoveState(this, stateMachine, "Move");
        jumpState = new KH_PlayerJumpState(this, stateMachine, "Jump");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
