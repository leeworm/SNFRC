using UnityEngine;

public class DH_EnemyJumpState : DH_EnemyState
{
    private float currentXVelocity;

    public DH_EnemyJumpState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName, float initialXVelocity)
        : base(_enemy, _stateMachine, _animBoolName)
    {
        currentXVelocity = initialXVelocity;
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

        if (xInput != 0)
        {
            float speedMagnitude = Mathf.Max(Mathf.Abs(currentXVelocity), enemy.moveSpeed);
            float xSpeed = speedMagnitude * xInput;

            // 속도가 0이면 → 기본 이동 속도로 덮어쓰기
            if (speedMagnitude == 0)
                speedMagnitude = enemy.moveSpeed;

            rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
            enemy.lastXVelocity = xSpeed;
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            stateMachine.ChangeState(enemy.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && enemy.currentJumpCount > 0)
        {
            Debug.Log("점프에서 점프로 전이");
            stateMachine.ChangeState(enemy.jumpState);
            return;
        }

        // 낙하 시작되면 AirState로 전이
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
