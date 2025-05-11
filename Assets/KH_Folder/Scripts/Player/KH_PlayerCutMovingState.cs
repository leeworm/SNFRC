using UnityEngine;

public class KH_PlayerCutMovingState : KH_PlayerState
{
    public KH_PlayerCutMovingState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(2.0f, 0.0f);
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
