using UnityEngine;

public class HK_PlayerJumpState : HK_PlayerState
{
    public HK_PlayerJumpState(HK_Player _player, HK_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0)
            stateMachine.ChangeState(player.airState);


    }
}