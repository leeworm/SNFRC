using UnityEngine;

public class DH_PlayerDashState : DH_PlayerGroundedState
{
    private float direction;

    public DH_PlayerDashState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName, float _direction)
        : base(_player, _stateMachine, _animBoolName)
    {
        this.direction = _direction;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(direction * player.dashSpeed, 0);
    }

    public override void Update()
    {
        base.Update();

        // 입력이 없으면 즉시 Idle로 전환
        if (xInput == 0)
        {
            stateMachine.ChangeState(new DH_PlayerIdleState(player, stateMachine, "Idle"));
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.CommandDetector.Reset();
    }
}
