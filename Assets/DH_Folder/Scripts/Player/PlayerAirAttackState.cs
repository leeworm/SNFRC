using System.Collections;
using UnityEngine;

public class PlayerAirAttackState : PlayerAirState
{

    private bool landingQueued = false;

    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        if (player.hasAirAttacked)
        {
            stateMachine.ChangeState(player.airState);
            return;
        }
        landingQueued = false;
        player.hasAirAttacked = true;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGrounded())
            landingQueued = true;

        if (triggerCalled && player.hasAirAttacked == true)
        {
            player.isBusy = false;
            triggerCalled = false;

            if (landingQueued && player.IsGrounded())
            {
                player.isLanding = true;
                player.anim.SetBool("Land", true);
                stateMachine.ChangeState(new PlayerLandState(player, stateMachine, "Land"));
            }
            else
                stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        triggerCalled = false;
    }
}
