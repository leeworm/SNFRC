using UnityEngine;

public class KoopaRoundFireState : KoopaState
{
    int roundfireCounting; // 발사한 불꽃의 개수

    public KoopaRoundFireState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        roundfireCounting = 0;
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        if(roundfireCounting >= koopa.roundFireCount)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        if(stateTimer <= 0)
        {
            koopa.RoundFire();
            
            roundfireCounting++;
            stateTimer = koopa.roundFireDelay;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
