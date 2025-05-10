using UnityEngine;

public class KH_PlayerState
{
    protected KH_PlayerStateMachine stateMachine;
    protected KH_Player player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;


    public KH_PlayerState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z) && xInput == 0 && !player.isStage1)
            stateMachine.ChangeState(player.shotState);
        else if (Input.GetKeyDown(KeyCode.X) && xInput == 0 && !player.isStage1 && player.setPipeTimer <= 0)
            stateMachine.ChangeState(player.setPipeState);
        else if (Input.GetKeyDown(KeyCode.C) && !player.isStage1 && player.mushRoomTimer <= 0)
        {
            player.CallMushRoom();

            player.mushRoomTimer = player.mushRoomCoolTime;
        }
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }   

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
