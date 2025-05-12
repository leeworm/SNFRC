using UnityEngine;

public class DH_EnemyDashAttackState : DH_EnemyGroundedState
{
    public DH_EnemyDashAttackState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true;
        enemy.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }
}