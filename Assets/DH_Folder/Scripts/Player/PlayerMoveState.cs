using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount = player.maxJumpCount;
        player.commandDetectorEnabled = true;
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("Checking dash input...");

        var dashType = player.CommandDetector.CheckCommand(player.facingDir, enabled: player.commandDetectorEnabled);

        if (dashType == CommandDetector.DashType.Forward)
        {
            //Debug.Log("Forward dash detected!");
            stateMachine.ChangeState(new PlayerDashState(player, stateMachine, "Dash", player.facingDir));
            return;
        }
        else if (dashType == CommandDetector.DashType.Backward)
        {
            //Debug.Log("Backstep detected!");
            stateMachine.ChangeState(new PlayerBackstepState(player, stateMachine, "Backstep", -player.facingDir));
            return;
        }


        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocity.y);
        if (xInput == 0 || player.IsWallDetected())
        { 
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
            return;
        }

        if (Input.GetButtonDown("Jump") && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump", player.lastXVelocity));
            return;
        }

        if (yInput < 0)
        {
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
            return;
        }
    }
}
