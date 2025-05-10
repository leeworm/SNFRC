using UnityEngine;

public class KH_PlayerMoveState : KH_PlayerGroundedState
{
    public KH_PlayerMoveState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName)
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


        player.SetVelocity(xInput*player.moveSpeed, rb.linearVelocityY);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);



    }


    public override void Exit()
    {
        base.Exit();
        player.SetZeroVelocity();
    }
}
