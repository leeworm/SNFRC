using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    private float direction;

    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, float _direction)
        : base(_player, _stateMachine, _animBoolName)
    {
        this.direction = _direction;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(direction * player.dashSpeed, 0);
        player.anim.SetBool("Dash", true); // 명시적으로 시작
    }

    public override void Update()
    {
        base.Update();

        // 입력이 없으면 즉시 Idle로 전환
        if (xInput == 0)
        {
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("Dash", false);
        player.dashCommandDetector.Reset();
    }
}
