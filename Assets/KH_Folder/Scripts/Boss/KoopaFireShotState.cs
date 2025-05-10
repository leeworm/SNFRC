using UnityEngine;

public class KoopaFireShotState : KoopaState
{
    int fireShotCounting; // 발사한 불꽃의 개수

    public KoopaFireShotState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fireShotCounting = 0;
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        if(fireShotCounting >= koopa.fireShotCount)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        if(stateTimer <= 0)
        {
            koopa.ShotFire();
            
            fireShotCounting++;
            stateTimer = koopa.fireShotDelay;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
