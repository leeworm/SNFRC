using System.Collections;
using UnityEngine;

public class DH_EnemyAirDefenseState : DH_EnemyAirState
{

    public DH_EnemyAirDefenseState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBlocking = true;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isGrounded)
        {
            stateMachine.ChangeState(enemy.landState);
            return;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            stateMachine.ChangeState(enemy.airState);
            return;
        }

    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
        enemy.isBlocking = false;
    }
}
