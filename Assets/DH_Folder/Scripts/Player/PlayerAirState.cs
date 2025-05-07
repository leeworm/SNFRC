using UnityEngine;

public class PlayerAirState : PlayerState
{
    private float currentXVelocity;

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        currentXVelocity = player.lastXVelocity; // 직전 속도 유지
        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.X) && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump", player.lastXVelocity));
            return;
        }

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.landState);

        if (xInput != 0)
            player.SetVelocity(currentXVelocity * xInput, rb.linearVelocity.y);
        else
        {
            currentXVelocity = 0f;
        }

        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }


    protected bool IsGrounded()
    {
        return player.IsGroundDetected();
    }
}
