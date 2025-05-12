using System.Collections;
using UnityEngine;

public class DH_EnemyKnockdownState : DH_EnemyState
{
    public DH_EnemyKnockdownState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.Play("Knockdown");
        enemy.SetVelocity(0, 0);
        enemy.StartCoroutine(KnockdownRecovery());
    }

    private IEnumerator KnockdownRecovery()
    {
        yield return new WaitForSeconds(0.5f); // 넉다운 지속 시간
        stateMachine.ChangeState(enemy.idleState);
    }
}
