using UnityEngine;

public class KH_PlayerInPipeState : KH_PlayerState
{
    public KH_PlayerInPipeState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        rb.linearVelocity = new Vector2(0, 0);

        
    }

    public override void Update()
    {
        base.Update();

        // 파이프 이동 끝나면 -> idle State
    }

    public override void Exit()
    {
        base.Exit();
    }
}
