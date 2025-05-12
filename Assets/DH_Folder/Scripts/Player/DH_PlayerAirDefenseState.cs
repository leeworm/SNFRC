using System.Collections;
using UnityEngine;

public class DH_PlayerAirDefenseState : DH_PlayerAirState
{

    public DH_PlayerAirDefenseState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBlocking = true;
    }

    public override void Update()
    {
        base.Update();

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.landState);
            return;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            stateMachine.ChangeState(player.airState);
            return;
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        player.isBlocking = false;
    }
}
