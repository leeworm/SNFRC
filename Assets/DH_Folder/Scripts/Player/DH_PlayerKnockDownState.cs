using System.Collections;
using UnityEngine;

public class DH_PlayerKnockdownState : DH_PlayerState
{
    public DH_PlayerKnockdownState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.anim.Play("Knockdown");
        player.SetVelocity(0, 0);
        player.StartCoroutine(KnockdownRecovery());
    }

    private IEnumerator KnockdownRecovery()
    {
        yield return new WaitForSeconds(0.5f); // 넉다운 지속 시간
        stateMachine.ChangeState(player.idleState);
    }
}
