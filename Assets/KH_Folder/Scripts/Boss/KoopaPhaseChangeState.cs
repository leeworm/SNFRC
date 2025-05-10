using UnityEngine;

public class KoopaPhaseChangeState : KoopaState
{
    public KoopaPhaseChangeState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
