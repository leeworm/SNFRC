using UnityEngine;

public class DH_EnemyIdleState : DH_EnemyState
{
    public DH_EnemyIdleState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
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
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
