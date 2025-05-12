using UnityEngine;

public class DH_EnemyMoveState : DH_EnemyGroundedState
{
    public DH_EnemyMoveState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isMoving = true;
        enemy.currentJumpCount = enemy.maxJumpCount;
        enemy.commandDetectorEnabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isMoving = false;
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log("Checking dash input...");

        var dashType = enemy.CommandDetector.CheckCommand(enemy.facingDir, enabled: enemy.commandDetectorEnabled);

        if (dashType == DH_CommandDetector.DashType.Forward)
        {
            //Debug.Log("Forward dash detected!");
            stateMachine.ChangeState(new DH_EnemyDashState(enemy, stateMachine, "Dash", enemy.facingDir));
            return;
        }
        else if (dashType == DH_CommandDetector.DashType.Backward)
        {
            //Debug.Log("Backstep detected!");
            stateMachine.ChangeState(new DH_EnemyBackstepState(enemy, stateMachine, "Backstep", -enemy.facingDir));
            return;
        }

        enemy.SetVelocity(xInput * enemy.moveSpeed, rb.linearVelocity.y);
        
        if (xInput == 0 ) //enemy.IsWallDetected()
        { 
            stateMachine.ChangeState(new DH_EnemyIdleState(enemy, stateMachine, "Idle"));
            return;
        }

        if (Input.GetButtonDown("Jump") && enemy.currentJumpCount > 0)
        {
            stateMachine.ChangeState(new DH_EnemyJumpState(enemy, stateMachine, "Jump", enemy.lastXVelocity));
            return;
        }

        if (yInput < 0)
        {
            stateMachine.ChangeState(new DH_EnemyCrouchState(enemy, stateMachine, "Crouch"));
            return;
        }
    }
}
