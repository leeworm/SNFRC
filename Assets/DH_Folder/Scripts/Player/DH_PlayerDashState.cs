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
        player.isDashing = true;
        player.SetVelocity(direction * player.dashSpeed, 0);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            stateMachine.ChangeState(player.dashAttackState);
            return;
        }

        // 입력이 없으면 즉시 Idle로 전환
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.isDashing = false;
        player.CommandDetector.Reset();
    }
}
