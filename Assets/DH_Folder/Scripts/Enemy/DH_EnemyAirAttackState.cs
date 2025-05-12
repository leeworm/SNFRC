using System.Collections;
using UnityEngine;

public class DH_EnemyAirAttackState : DH_EnemyAirState
{

    private bool landingQueued = false;

    public DH_EnemyAirAttackState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        if (enemy.isAttackingAir)
        {
            stateMachine.ChangeState(enemy.airState);
            return;
        }
        landingQueued = false;
        enemy.isAttackingAir = true;
    }

    public override void Update()
    {
        base.Update();

        // 1. 공중공격 중 땅에 닿았으면 landingQueued를 true로
        if (enemy.isGrounded)
            landingQueued = true;

        // 2. 애니메이션이 끝났을 때
        if (triggerCalled)
        {
            enemy.isBusy = false;
            triggerCalled = false;
            enemy.anim.SetBool("AirAttack", false);

            if (landingQueued && enemy.isGrounded)
            {
                // 땅에 닿은 적이 있고, 현재도 땅에 닿아 있으면 랜드 상태로
                stateMachine.ChangeState(enemy.landState);
            }
            else
            {
                // 아직 공중이면 에어 상태로
                stateMachine.ChangeState(enemy.airState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
