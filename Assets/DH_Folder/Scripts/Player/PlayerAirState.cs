using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.landState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * xInput, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }


    protected bool IsGrounded()
    {
        return player.IsGroundDetected();
    }
}
