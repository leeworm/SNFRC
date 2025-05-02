using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        if (player.currentJumpCount > 0)
        {
            player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
            player.currentJumpCount--;
        }
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);
        player.FlipController(xInput);

        if (rb.linearVelocity.y <= 0)
        {
            stateMachine.ChangeState(new PlayerFallState(player, stateMachine, "JumpFall"));
        }
    }
}
