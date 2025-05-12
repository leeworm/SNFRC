using UnityEngine;
using static DH_CommandDetector;

public class DH_EnemyLandState : DH_EnemyGroundedState
{
    private float commandBufferTime = 0.15f;
    private float commandBufferTimer;
    private bool bufferingInput = false;

    public DH_EnemyLandState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0, rb.linearVelocity.y);
        enemy.isLanding = true;
        enemy.isAttackingAir = false;
        enemy.commandDetectorEnabled = true;
        bufferingInput = false;
        commandBufferTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(0, rb.linearVelocity.y);
        
        if (triggerCalled)
        {
            enemy.isLanding = false;
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        var commandType = enemy.CommandDetector.CheckCommand(enemy.facingDir, enabled: enemy.commandDetectorEnabled);

        if (commandType == DashType.Forward)
        {
            stateMachine.ChangeState(enemy.dashState);
            return;
        }
        if (commandType == DashType.Backward)
        {
            stateMachine.ChangeState(enemy.backstepState);
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
                stateMachine.ChangeState(enemy.moveState);
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
        enemy.isLanding = false;
        enemy.commandDetectorEnabled = false;
    }
}
