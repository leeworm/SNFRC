using UnityEngine;
using UnityEngine.Windows;

public class HK_PlayerIdleState : HK_PlayerGroundedState
{
    public HK_PlayerIdleState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
    }
    public override void Update()
    {
        base.Update();



        if (xInput == player.facingDir && player.IsWallDetected())
            return;




        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);



    }
    public override void Exit()
    {
        base.Exit();
    }


}
