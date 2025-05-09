using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float currentXVelocity;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, float initialXVelocity)
        : base(_player, _stateMachine, _animBoolName)
    {
        currentXVelocity = initialXVelocity;
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        currentXVelocity = player.lastXVelocity; // 직전 속도 유지
        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
        if (player.currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(currentXVelocity, player.jumpForce);
            player.currentJumpCount--;
        }
    }

    public override void Update()
    {
        base.Update();

        if (xInput > 0)
        {
            rb.linearVelocity = new Vector2(xInput * currentXVelocity, rb.linearVelocity.y);
            player.lastXVelocity = rb.linearVelocity.x; // 마지막 X 속도 저장
        }
        if (xInput < 0)
        {
            rb.linearVelocity = new Vector2(-xInput * currentXVelocity, rb.linearVelocity.y);
            player.lastXVelocity = rb.linearVelocity.x;
        }
        if (xInput == 0)
        {
            rb.linearVelocity = new Vector2(xInput * player.moveSpeed, rb.linearVelocity.y);
            player.lastXVelocity = rb.linearVelocity.x;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            stateMachine.ChangeState(new PlayerAirAttackState(player, stateMachine, "AirAttack"));
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && player.currentJumpCount > 0)
        {
            Debug.Log("점프에서 점프로 전이");
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump", player.lastXVelocity));
            return;
        }

        // 낙하 시작되면 AirState로 전이
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
    }
}
