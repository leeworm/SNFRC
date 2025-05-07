using UnityEngine;

public class PlayerPrimaryAttackState : PlayerGroundedState
{
    private float lastTimeAttacked;
    private float comboWindow = 0.5f;
    private bool canCombo;
    private bool attackFinished;

    public int comboCounter { get; private set; }

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 콤보 초기화 조건
        if (player.primaryAttackComboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            player.primaryAttackComboCounter = 0;
        }

        comboCounter = player.primaryAttackComboCounter;
        player.anim.SetInteger("ComboCounter", comboCounter);

        // 입력 방향 기반으로 공격 방향 처리
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;

        player.SetZeroVelocity();
        player.isBusy = true;

        attackFinished = false;
        canCombo = true;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState); // 애니메이션 종료 트리거 호출 시 대기 상태로 전환
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f); // 짧은 시간 행동 제한
        lastTimeAttacked = Time.time;
        player.isBusy = false;
    }

    #region Animation Events Called via AnimationTriggers
    public void EnableComboWindow() => canCombo = true;
    public void DisableComboWindow() => canCombo = false;
    public void OnAttackComplete() => attackFinished = true;
    #endregion
}
