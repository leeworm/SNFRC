using UnityEngine;

public class KoopaFireShotState : KoopaState
{
    int fireCount; // 발사한 불꽃의 개수

    public KoopaFireShotState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fireCount = 0;
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        if(fireCount >= 3)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        if(stateTimer <= 0)
        {
            koopa.ShotFire();
            
            fireCount++;
            stateTimer = 0.7f;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
