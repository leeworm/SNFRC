using UnityEngine;

public abstract class DH_EnemyGroundedState : DH_EnemyState
{
    public DH_EnemyGroundedState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isGrounded = true;
        enemy.currentJumpCount = enemy.maxJumpCount;
        enemy.CommandDetector.Reset();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isGrounded = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.DownArrow) && yInput < 0)
        {
            if (enemy.isBusy || enemy.isAttacking)
                return;
            stateMachine.ChangeState(enemy.crouchState);
            return;
        }

        // 공격 입력 처리 메서드로 분리
        if (HandleAttackInput())
            return;

        if (Input.GetKeyDown(KeyCode.X)
            && (enemy.currentJumpCount > 0 && (!enemy.isBusy && !enemy.isAttacking)))
        {
            if (yInput > 0.1f && enemy.isIdle)
                stateMachine.ChangeState(enemy.teleportJumpState);
            else
                stateMachine.ChangeState(enemy.jumpState);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (enemy.isAttacking || enemy.isSubstituting || enemy.isBusy || enemy.isBlocking)
                return;
            stateMachine.ChangeState(enemy.defenseState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && !enemy.isBusy)
        {
            stateMachine.ChangeState(enemy.SexyJutsuState);
            return;
        }

        if (enemy.isLanding && enemy.isGrounded && !(stateMachine.currentState is DH_EnemyLandState) && !enemy.isSubstituting)
        {
            stateMachine.ChangeState(enemy.landState);
            return;
        }

        if (enemy.isIdle)
        {
            if (enemy.isIdle)
                return;
            else
                stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }

    /// <summary>
    /// 공격 관련 입력을 한 번에 처리 (우선순위: 어퍼컷 > 대시어택 > 일반공격)
    /// </summary>
    /// <returns>공격 입력이 처리되면 true, 아니면 false</returns>
    private bool HandleAttackInput()
    {
        if (!Input.GetKeyDown(KeyCode.Z))
            return false;

        // 공통 예외 처리
        if (enemy.isBusy || enemy.isBlocking || enemy.isSubstituting )
            return true; // 입력은 있었으나 무시

        // 어퍼컷(위+Z)이 최우선
        if (yInput > 0.1f)
        {
            if (enemy.isAttacking)
                return true;
            enemy.primaryAttackComboCounter = 0;
            stateMachine.ChangeState(enemy.uppercutState);
            return true;
        }

        // 대시어택(대시 중 Z)
        if (enemy.isDashing)
        {
            stateMachine.ChangeState(enemy.dashAttackState);
            return true;
        }

        // 일반공격(중립 Z)
        if (!(yInput > 0.1f) && !(yInput < 0))
        {
            stateMachine.ChangeState(enemy.primaryAttack);
            return true;
        }

        return false;
    }
}
