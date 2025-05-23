using UnityEngine;

public class HK_PlayerDashState : HK_PlayerState
{
    public HK_PlayerDashState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        

        stateTimer = player.dashDuration;
    }


    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);


        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }


    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocityY);

    }

}
