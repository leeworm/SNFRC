using UnityEngine;

public class DH_EnemyDefenseState : DH_EnemyGroundedState
{
    public DH_EnemyDefenseState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true;
        enemy.isBlocking = true;
        enemy.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
        enemy.isBlocking = false;
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }
}
