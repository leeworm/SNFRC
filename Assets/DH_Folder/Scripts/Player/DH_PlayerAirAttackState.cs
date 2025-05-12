using System.Collections;
using UnityEngine;

public class DH_PlayerAirAttackState : DH_PlayerAirState
{

    private bool landingQueued = false;

    public DH_PlayerAirAttackState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        if (player.isAttackingAir)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        landingQueued = false;
        player.isAttackingAir = true;
    }

    public override void Update()
    {
        base.Update();

        // 1. 공중공격 중 땅에 닿았으면 landingQueued를 true로
        if (player.isGrounded)
            landingQueued = true;

        // 2. 애니메이션이 끝났을 때
        if (triggerCalled)
        {
            player.isBusy = false;
            triggerCalled = false;
            player.anim.SetBool("AirAttack", false);

            if (landingQueued && player.isGrounded)
            {
                // 땅에 닿은 적이 있고, 현재도 땅에 닿아 있으면 랜드 상태로
                stateMachine.ChangeState(player.landState);
            }
            else
            {
                // 아직 공중이면 에어 상태로
                stateMachine.ChangeState(player.airState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
