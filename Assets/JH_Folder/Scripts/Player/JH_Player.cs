using UnityEngine;

public class JH_Player : JH_Entity
{

    public JH_PlayerStateMachine StateMachine { get; private set; }
    #region States
    public JH_PlayerIdleState IdleState { get; private set; }
    public JH_PlayerMoveState MoveState { get; private set; }
    public JH_PlayerJumpState JumpState { get; private set; }
    //public JH_PlayerAirState AirState { get; private set; }
    public JH_PlayerCrouchState CrouchState { get; private set; }
    public JH_PlayerGuardState GuardState { get; private set; }
    public JH_PlayerForwardJumpState ForwardJumpState { get; private set; }
    public JH_PlayerBackJumpState BackJumpState { get; private set; }
    public JH_PlayerGroundedAttackState GroundedAttackState { get; private set; } 
    public JH_PlayerCrouchAttackState CrouchAttackState { get; private set; }
    public JH_PlayerJumpAttackState JumpAttackState { get; private set; }
    public JH_PlayerFBJumpAttackState FBJumpAttackState { get; private set; }
    public JH_PlayerGigongState GigongState { get; private set; }

    #endregion

    public int LastAttackIndex { get; set; }
    


    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new JH_PlayerStateMachine();

        IdleState = new JH_PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new JH_PlayerMoveState(this, StateMachine, "Move");
        JumpState = new JH_PlayerJumpState(this, StateMachine, "Jump");
        //AirState = new JH_PlayerAirState(this, StateMachine, "Air");
        CrouchState = new JH_PlayerCrouchState(this, StateMachine, "Crouch");
        GuardState = new JH_PlayerGuardState(this, StateMachine, "Guard");
        ForwardJumpState = new JH_PlayerForwardJumpState(this, StateMachine, "ForwardJump");
        BackJumpState = new JH_PlayerBackJumpState(this, StateMachine, "BackJump");
        GroundedAttackState = new JH_PlayerGroundedAttackState(this, StateMachine, "");
        CrouchAttackState = new JH_PlayerCrouchAttackState(this, StateMachine, "");
        JumpAttackState = new JH_PlayerJumpAttackState(this, StateMachine, "");
        FBJumpAttackState = new JH_PlayerFBJumpAttackState(this, StateMachine, "");
        GigongState = new JH_PlayerGigongState(this, StateMachine, "Gigong");
    }

   
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);

    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }

    protected override void Exit()
    {
        base.Exit();
    }
}
