using UnityEngine;

public class KH_PlayerJumpState : KH_PlayerState
{
    public KH_PlayerJumpState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(rb.linearVelocityX, player.jumpForce);

        stateTimer = 0.5f;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected() && stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.linearVelocityY);

    }

    public override void Exit()
    {
        base.Exit();
    }

}
