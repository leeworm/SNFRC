using UnityEngine;

public class DH_EnemyMoveState : DH_EnemyState
{
    public DH_EnemyMoveState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isMoving = true;
        enemy.currentJumpCount = enemy.maxJumpCount;
        enemy.commandDetectorEnabled = true;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isAttackInput)
        {
            enemy.isAttackInput = false;
            stateMachine.ChangeState(enemy.primaryAttack);
            return;
        }
        if (enemy.isJumpInput)
        {
            enemy.isJumpInput = false;
            stateMachine.ChangeState(enemy.jumpState);
        }
        if (enemy.isDashInput)
        {
            enemy.isDashInput = false;
            stateMachine.ChangeState(enemy.dashState);
        }
        if (enemy.inputX == 0)
        {
            stateMachine.ChangeState(enemy.idleState); // ← 반대 전이도 명시
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isMoving = false;
    }
}
