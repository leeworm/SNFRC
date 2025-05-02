using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        if (player.currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpForce);
            player.currentJumpCount--;
        }
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
