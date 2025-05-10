using UnityEngine;

public class HK_PlayerWallSlideState : HK_PlayerState
{
    public HK_PlayerWallSlideState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }





        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY * 0.7f);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);


    }
}
