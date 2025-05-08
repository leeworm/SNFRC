using UnityEngine;

public class KH_PlayerSetPipeState : KH_PlayerState
{
    public KH_PlayerSetPipeState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
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
