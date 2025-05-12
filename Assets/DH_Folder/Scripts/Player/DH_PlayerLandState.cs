using UnityEngine;
using static DH_CommandDetector;

public class DH_PlayerLandState : DH_PlayerGroundedState
{
    private float commandBufferTime = 0.15f;
    private float commandBufferTimer;
    private bool bufferingInput = false;

    public DH_PlayerLandState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.linearVelocity.y);
        player.isLanding = true;
        player.isAttackingAir = false;
        player.commandDetectorEnabled = true;
        bufferingInput = false;
        commandBufferTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocity.y);
        
        if (triggerCalled)
        {
            player.isLanding = false;
            stateMachine.ChangeState(player.idleState);
            return;
        }

        var commandType = player.CommandDetector.CheckCommand(player.facingDir, enabled: player.commandDetectorEnabled);

        if (commandType == DashType.Forward)
        {
            stateMachine.ChangeState(player.dashState);
            return;
        }
        if (commandType == DashType.Backward)
        {
            stateMachine.ChangeState(player.backstepState);
            return;
        }

        if (xInput != 0)
        {
            if (!bufferingInput)
            {
                bufferingInput = true;
                commandBufferTimer = commandBufferTime;
                return;
            }

            commandBufferTimer -= Time.deltaTime;

            if (commandBufferTimer <= 0f)
            {
                stateMachine.ChangeState(player.moveState);
                return;
            }
        }
        else
        {
            bufferingInput = false;
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.isLanding = false;
        player.commandDetectorEnabled = false;
    }
}
