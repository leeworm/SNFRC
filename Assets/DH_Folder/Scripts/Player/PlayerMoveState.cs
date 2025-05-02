using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, "Move") { }

    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount = player.maxJumpCount;
    }

    public override void Update()
    {
        base.Update();

        int dashDir;
        if (player.dashCommandDetector.CheckDashCommand(out dashDir))
        {
            stateMachine.ChangeState(new PlayerDashState(player, stateMachine, dashDir));
            return;
        }

        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);
        player.FlipController(xInput);

        if (xInput == 0)
        {
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
        }

        if (Input.GetButtonDown("Jump") && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump"));
        }

        if (yInput < 0)
        {
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
        }
    }
}
