using UnityEngine;

public class DH_EnemyAirState : DH_EnemyState
{
    private float currentXVelocity;

    public DH_EnemyAirState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        // 1. X속도 깔끔히 정리: 일정 이하로 작으면 0으로 고정
        float rawX = rb.linearVelocity.x;
        float fixedX = Mathf.Abs(rawX) < 0.05f ? 0f : rawX;
        currentXVelocity = fixedX;
        //Debug.Log($"💥 AirState 진입 시점 - rb.linearVelocity.x: {rb.linearVelocity.x}, currentXVelocity: {currentXVelocity}");
        if (Mathf.Abs(currentXVelocity) > 0.1f)
            enemy.FlipController(currentXVelocity);
        enemy.SetVelocity(currentXVelocity, rb.linearVelocityY);
        enemy.commandDetectorEnabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (enemy.isBlocking
                || enemy.isAttackingAir
                || enemy.isSubstituting
                || enemy.isBusy)
                return;
            
            stateMachine.ChangeState(enemy.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X)
            && enemy.currentJumpCount > 0 
            && !enemy.isBlocking
            && !enemy.isSubstituting)
        {
            Debug.Log("에어에서 점프로 전이");
            stateMachine.ChangeState(enemy.jumpState);
            return;
        }

        if (Input.GetKey(KeyCode.S) || enemy.isBlocking)
        {
            stateMachine.ChangeState(enemy.airDefenseState);
            return;
        }

        if (enemy.isGrounded)
        {
            if (enemy.isSubstituting)
                return;
            stateMachine.ChangeState(enemy.landState);
            return;
        }

        // 방향키 입력 체크 후 수평 속도 적용
        if (xInput != 0)
        {
            // 방향키 누르고 있으면 이전 속도 유지
            enemy.SetVelocity(currentXVelocity, rb.linearVelocity.y);
        }
        else
        {
            // 입력 없으면 뚝 멈추기 (부동소수점 오차 방지)
            enemy.SetVelocity(0f, rb.linearVelocity.y);
        }


        enemy.SetVelocity(currentXVelocity, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
