using System.Collections;
using UnityEngine;

public class PlayerAirDefenseState : PlayerAirState
{

    public PlayerAirDefenseState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.isBlocking = true;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.S) && IsGrounded())
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
