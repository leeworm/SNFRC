using UnityEngine;

public class DH_EnemyDashState : DH_EnemyState
{
    private float direction;
    private Transform player;
    private float dashDuration = 0.25f; // 대시 유지 시간
    private float dashSpeed => enemy.dashSpeed;
    private float endTime;
    private float attackDistance = 2.5f;

    public DH_EnemyDashState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName, float _direction)
        : base(enemy, stateMachine, animBoolName)
    {
        this.direction = _direction;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isDashing = true;
        enemy.anim.SetBool("Dash", true);
        endTime = Time.time + dashDuration;
        // 👉 AI가 설정한 dashDir 방향으로 속도 적용
        enemy.SetVelocity(enemy.dashDir * enemy.dashSpeed, 0f);

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void Update()
    {
        base.Update();

        // 일정 시간 유지되면 전이 가능
        if (Time.time >= endTime)
        {
            if (enemy.IsGrounded())
            {
                stateMachine.ChangeState(enemy.idleState);
                return;
            }
        }

        // 대시 도중 공격 거리 진입 시 공격으로 전환
        if (player != null)
        {
            float distance = Vector2.Distance(enemy.transform.position, player.position);
            if (distance <= attackDistance)
            {
                enemy.SetZeroVelocity();
                enemy.isAttackInput = true; // 다음 프레임에 공격 전이되도록 준비
                return;
            }
        }


        // 예시 자동 전이 조건들 (필요한 상태만 활성화)
        if (enemy.isAttackInput)
        {
            enemy.isAttackInput = false;
            stateMachine.ChangeState(enemy.primaryAttack);
        }
        if (enemy.isJumpInput)
        {
            enemy.isJumpInput = false;
            stateMachine.ChangeState(enemy.jumpState);
        }
        if (enemy.isDashInput)
        {
            enemy.isDashInput = false;
            stateMachine.ChangeState(enemy.dashState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("Dash", false);
        enemy.isDashing = false;
    }
}
