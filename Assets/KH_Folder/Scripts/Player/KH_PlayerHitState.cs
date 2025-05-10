using System.Collections;
using UnityEngine;

public class KH_PlayerHitState : KH_PlayerState
{
    public KH_PlayerHitState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.canHit = false;

        // 깜빡
        player.StartCoroutine(player.Flicker());

        stateTimer = 1.0f;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.canHit = true;
    }

    
}