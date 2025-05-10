using UnityEngine;
using UnityEngine.Windows;

public class HK_PlayerMoveState : HK_PlayerGroundedState
{
    public HK_PlayerMoveState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }



    public override void Update()
    {
        base.Update();


        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocityY);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);



    }


    public override void Exit()
    {
        base.Exit();
    }
}
