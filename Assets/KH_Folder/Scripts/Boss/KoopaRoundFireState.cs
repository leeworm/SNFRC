using UnityEngine;

public class KoopaRoundFireState : KoopaState
{
    int roundfireCount; // 발사한 불꽃의 개수

    public KoopaRoundFireState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        roundfireCount = 0;
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();
        
        if(roundfireCount >= 2)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        if(stateTimer <= 0)
        {
            koopa.RoundFire();
            
            roundfireCount++;
            stateTimer = 1f;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
