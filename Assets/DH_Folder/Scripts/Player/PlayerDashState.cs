using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    private int direction;
    private float dashDuration = 0.2f;
    private float dashTimer;

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, int _direction)
        : base(_player, _stateMachine, "Dash")
    {
        this.direction = _direction;
    }

    public override void Enter()
    {
        base.Enter();
        dashTimer = dashDuration;
        player.SetVelocity(direction * player.dashSpeed, 0);
    }

    public override void Update()
    {
        base.Update();
        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
        }
    }
}
