using UnityEngine;

public class KoopaIdleState : KoopaState
{
    public KoopaIdleState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        rb.linearVelocity = new Vector2(0, 0);

        stateTimer = 2f;
    }

    public override void Update()
    {
        base.Update();

        if(koopa.phaseState == PhaseState.PhaseChange)
        {
            KH_GameManager.Instance.PhaseChange();
        }

        if (stateTimer <= 0 && koopa.phaseState != PhaseState.PhaseChange) // 페이즈 1 이나 페이즈 2로 바뀌면
        {
            stateMachine.ChangeState(koopa.walkState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
