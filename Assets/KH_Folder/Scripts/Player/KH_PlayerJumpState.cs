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

        // 점프 중에 x 축 움직임
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.6f * xInput, rb.linearVelocityY);
    
        // 낙하 중일 때만 적 밟기 체크
        if (player.rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState); // 낙하 상태로 전환
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
