using UnityEngine;

public class KoopaDeathState : KoopaState
{
    public KoopaDeathState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        koopa.TriggerOn();

        rb.linearVelocityY = -koopa.deathJumpPower;

        koopa.CreateErrorPiece();
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
