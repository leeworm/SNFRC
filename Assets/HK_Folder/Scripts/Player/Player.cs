using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class Player : Entity
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
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    #region States
    // 플레이어의 상태를 관리하는 상태 머신
    public PlayerStateMachine stateMachine { get; private set; }

    // 플레이어의 상태 (대기 상태, 이동 상태)
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }


    #endregion


    protected override void Awake()
    {
        base.Awake();

        // 상태 머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerFallState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");



    }

    protected override void Start()
    {

        base.Start();


        skill = SkillManager.instance;

        // 게임 시작 시 초기 상태를 대기 상태(idleState)로 설정
        stateMachine.Initialize(idleState);

        


    }




    protected override void Update()
    {
        base.Update();
        if (stateMachine.currentState != null)
        {
            stateMachine.currentState.Update();
        }
        else
        {
            Debug.LogError("Current state is null!");
        }

        CheckForDashInput();
    }




    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;


        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }






    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {

        if (SkillManager.instance != null && SkillManager.instance.dash != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
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