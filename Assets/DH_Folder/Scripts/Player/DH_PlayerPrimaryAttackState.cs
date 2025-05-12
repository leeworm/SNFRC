using UnityEngine;

public class DH_PlayerPrimaryAttackState : DH_PlayerGroundedState
{
    private bool canCombo;
    private bool attackFinished;

    public int comboCounter { get; private set; }

    public DH_PlayerPrimaryAttackState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        comboCounter = player.primaryAttackComboCounter;


        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;

        player.SetVelocity(0, rb.linearVelocity.y);
        player.isBusy = true;
        player.isAttacking = true;
        attackFinished = false;
        canCombo = true;

        // 버퍼는 매 상태 진입 시 초기화
        player.bufferedAttackInput = false;
    }


    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetVelocity(0, rb.linearVelocity.y);

        // 콤보 입력 감지 (단, 마지막 콤보면 입력 막기)
        if (canCombo && !attackFinished && player.primaryAttackComboCounter < 2 && Input.GetKeyDown(KeyCode.Z))
        {
            player.bufferedAttackInput = true;
            // Debug.Log("[Combo] 공격 입력이 버퍼에 저장됨");
        }

        if (triggerCalled)
        {

            if (player.bufferedAttackInput && player.primaryAttackComboCounter < 2)
            {
                if (player.IsGroundDetected())
                {
                    player.primaryAttackComboCounter++;
                    player.primaryAttackComboCounter = Mathf.Clamp(player.primaryAttackComboCounter, 0, 2);
                    // Debug.Log($"[Combo] comboCounter++ → {player.primaryAttackComboCounter}");
                }
                player.bufferedAttackInput = false;
                stateMachine.ChangeState(new DH_PlayerPrimaryAttackState(player, stateMachine, "Attack"));
            }
            else
            {
                player.primaryAttackComboCounter = 0;
                player.anim.SetInteger("ComboCounter", 0);
                player.bufferedAttackInput = false;
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f); // 짧은 시간 행동 제한
        player.isBusy = false;
        player.isAttacking = false;
    }

    #region Animation Events Called via AnimationTriggers
    public void EnableComboWindow() => canCombo = true;
    public void DisableComboWindow() => canCombo = false;
    public void OnAttackComplete() => attackFinished = true; //Debug.Log("[Combo] OnAttackComplete() 호출됨 → 공격 종료 처리");
    #endregion
}
