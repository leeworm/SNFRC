using UnityEngine;

public class DH_EnemyAirAttackState : DH_EnemyState
{
    public DH_EnemyAirAttackState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isGrounded)
        {
            stateMachine.ChangeState(enemy.landState);
        }

        // 예시 자동 전이 조건들 (필요한 상태만 활성화)
        if (enemy.isAttackInput)
        {
            enemy.isAttackInput = false;
            stateMachine.ChangeState(enemy.primaryAttack);
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}
