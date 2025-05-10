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
        
        player.SetPipe();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyUp(KeyCode.X))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        player.setPipeTimer = player.setPipeCoolTime; // 쿨타임 초기화
    }
}
