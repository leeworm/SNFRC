using UnityEngine;

public class DH_EnemyJumpState : DH_EnemyState
{
    private float currentXVelocity;

    public DH_EnemyJumpState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName)
    {
        currentXVelocity = 0f;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.isBusy = true;
        enemy.isJumping = true;

        currentXVelocity = rb.linearVelocity.x;

        if (enemy.currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(currentXVelocity, enemy.jumpForce);
            enemy.currentJumpCount--;
        }
    }

    public override void Update()
    {
        base.Update();

        // xInput 기반 공중 이동
        if (xInput != 0)
        {
            float speedMagnitude = Mathf.Max(Mathf.Abs(currentXVelocity), enemy.moveSpeed);
            float xSpeed = speedMagnitude * xInput;

            if (speedMagnitude == 0)
                speedMagnitude = enemy.moveSpeed;

            rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
            enemy.lastXVelocity = xSpeed;
        }

        // 이중 점프 (점프 입력이 다시 들어왔을 때)
        if (enemy.isJumpInput && enemy.currentJumpCount > 0)
        {
            enemy.isJumpInput = false;
            stateMachine.ChangeState(enemy.jumpState);
            return;
        }

        // 공격 입력 → 공중 공격 전환
        if (enemy.isAttackInput)
        {
            enemy.isAttackInput = false;
            stateMachine.ChangeState(enemy.airAttackState);
            return;
        }

        // 낙하 시작 → AirState 전환
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(enemy.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
        enemy.isJumping = false;
    }
}
