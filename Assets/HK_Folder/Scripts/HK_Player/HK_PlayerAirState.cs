using UnityEngine;

public class HK_PlayerAirState : HK_PlayerState
{
    public HK_PlayerAirState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlide);
            return;
        }



        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.linearVelocityY);


    }
}