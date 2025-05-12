using System.Collections;
using UnityEngine;

public class HK_Player : HK_Entity
{
    [Header("공격 디테일")]
    public Vector2[] attackMovement;

    public bool isBusy { get; private set; }

    [Header("이동 정보")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("대시 정보")]
    public float dashSpeed;
    public float dashDuration;
    internal bool inputEnabled;

    public float dashDir { get; private set; }

    public bool grounded => IsGroundDetected();
    public HK_SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    #region States
    public HK_PlayerStateMachine stateMachine { get; private set; }

    public HK_PlayerIdleState idleState { get; private set; }
    public HK_PlayerMoveState moveState { get; private set; }
    public HK_PlayerJumpState jumpState { get; private set; }
    public HK_PlayerFallState airState { get; private set; }
    public HK_PlayerDashState dashState { get; private set; }
    public HK_PlayerWallSlideState wallSlide { get; private set; }
    public HK_PlayerWallJumpState wallJump { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new HK_PlayerStateMachine();
        idleState = new HK_PlayerIdleState(this, stateMachine, "Idle");
        moveState = new HK_PlayerMoveState(this, stateMachine, "Move");
        jumpState = new HK_PlayerJumpState(this, stateMachine, "Jump");
        airState = new HK_PlayerFallState(this, stateMachine, "Jump");
        dashState = new HK_PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new HK_PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new HK_PlayerWallJumpState(this, stateMachine, "Jump");
    }

    protected override void Start()
    {
        base.Start();
        skill = HK_SkillManager.instance;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (stateMachine.currentState != null)
            stateMachine.currentState.Update();
        else
            Debug.LogError("Current state is null!");

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (HK_SkillManager.instance?.dash != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && HK_SkillManager.instance.dash.CanUseSkill())
            {
                dashDir = Input.GetAxisRaw("Horizontal");
                if (dashDir == 0)
                    dashDir = facingDir;

                stateMachine.ChangeState(dashState);
            }
        }
        else
        {
            Debug.LogError("SkillManager or Dash skill is not initialized properly.");
        }
    }
}