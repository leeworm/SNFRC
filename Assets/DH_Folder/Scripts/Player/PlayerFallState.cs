using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);
        player.FlipController(xInput);

        if (Input.GetButtonDown("Jump") && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump"));
            return;
        }

        if (IsGrounded())
        {
            player.currentJumpCount = player.maxJumpCount;

            if (xInput != 0)
                stateMachine.ChangeState(new PlayerMoveState(player, stateMachine, "Move"));
            else
                stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
        }
    }
}
