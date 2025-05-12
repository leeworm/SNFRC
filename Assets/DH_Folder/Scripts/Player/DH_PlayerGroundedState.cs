using UnityEngine;

public abstract class DH_PlayerGroundedState : DH_PlayerState
{
    public DH_PlayerGroundedState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isGrounded = true;
        player.currentJumpCount = player.maxJumpCount;
        player.CommandDetector.Reset();
    }

    public override void Exit()
    {
        base.Exit();
        player.isGrounded = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.DownArrow) && yInput < 0)
        {
            if (player.isBusy || player.isAttacking)
                return;
            stateMachine.ChangeState(player.crouchState);
            return;
        }

        // 공격 입력 처리 메서드로 분리
        if (HandleAttackInput())
            return;

        if (Input.GetKeyDown(KeyCode.X)
            && (player.currentJumpCount > 0 && (!player.isBusy && !player.isAttacking)))
        {
            if (yInput > 0.1f && player.isIdle)
                stateMachine.ChangeState(player.teleportJumpState);
            else
                stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (player.isAttacking || player.isSubstituting || player.isBusy || player.isBlocking)
                return;
            stateMachine.ChangeState(player.defenseState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.F) && !player.isBusy)
        {
            stateMachine.ChangeState(player.SexyJutsuState);
            return;
        }

        if (player.isLanding && player.isGrounded && !(stateMachine.currentState is DH_PlayerLandState) && !player.isSubstituting)
        {
            stateMachine.ChangeState(player.landState);
            return;
        }

        if (player.isIdle)
        {
            if (player.isIdle)
                return;
            else
                stateMachine.ChangeState(player.idleState);
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
        if (player.isBusy || player.isBlocking || player.isSubstituting )
            return true; // 입력은 있었으나 무시

        // 어퍼컷(위+Z)이 최우선
        if (yInput > 0.1f)
        {
            if (player.isAttacking)
                return true;
            player.primaryAttackComboCounter = 0;
            stateMachine.ChangeState(player.uppercutState);
            return true;
        }

        // 대시어택(대시 중 Z)
        if (player.isDashing)
        {
            stateMachine.ChangeState(player.dashAttackState);
            return true;
        }

        // 일반공격(중립 Z)
        if (!(yInput > 0.1f) && !(yInput < 0))
        {
            stateMachine.ChangeState(player.primaryAttack);
            return true;
        }

        return false;
    }
}
