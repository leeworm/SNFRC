using UnityEngine;

public class DH_EnemyDashState : DH_EnemyGroundedState
{
    private float direction;

    public DH_EnemyDashState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName, float _direction)
        : base(_enemy, _stateMachine, _animBoolName)
    {
        this.direction = _direction;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isDashing = true;
        enemy.SetVelocity(direction * enemy.dashSpeed, 0);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            stateMachine.ChangeState(enemy.dashAttackState);
            return;
        }

        // 입력이 없으면 즉시 Idle로 전환
        if (xInput == 0)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemy.isDashing = false;
    }
}
