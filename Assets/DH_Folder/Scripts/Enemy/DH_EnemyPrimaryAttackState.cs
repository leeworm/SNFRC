using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DH_EnemyPrimaryAttackState : DH_EnemyGroundedState
{
    private bool canCombo;
    private bool attackFinished;

    public int comboCounter { get; private set; }

    public DH_EnemyPrimaryAttackState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        comboCounter = enemy.primaryAttackComboCounter;


        enemy.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = enemy.facingDir;
        if (xInput != 0)
            attackDir = xInput;

        enemy.SetVelocity(0, rb.linearVelocity.y);
        enemy.isBusy = true;
        enemy.isAttacking = true;
        attackFinished = false;
        canCombo = true;

        // 버퍼는 매 상태 진입 시 초기화
        enemy.bufferedAttackInput = false;
    }


    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            enemy.SetVelocity(0, rb.linearVelocity.y);

        // 콤보 입력 감지 (단, 마지막 콤보면 입력 막기)
        if (canCombo && !attackFinished && enemy.primaryAttackComboCounter < 2 && Input.GetKeyDown(KeyCode.Z))
        {
            enemy.bufferedAttackInput = true;
            // Debug.Log("[Combo] 공격 입력이 버퍼에 저장됨");
        }

        if (triggerCalled)
        {

            if (enemy.bufferedAttackInput && enemy.primaryAttackComboCounter < 2)
            {
                if (enemy.IsGroundDetected())
                {
                    enemy.primaryAttackComboCounter++;
                    enemy.primaryAttackComboCounter = Mathf.Clamp(enemy.primaryAttackComboCounter, 0, 2);
                    // Debug.Log($"[Combo] comboCounter++ → {enemy.primaryAttackComboCounter}");
                }
                enemy.bufferedAttackInput = false;
                stateMachine.ChangeState(new DH_EnemyPrimaryAttackState(enemy, stateMachine, "Attack"));
            }
            else
            {
                enemy.primaryAttackComboCounter = 0;
                enemy.anim.SetInteger("ComboCounter", 0);
                enemy.bufferedAttackInput = false;
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StartCoroutine("BusyFor", 0.1f); // 짧은 시간 행동 제한
        enemy.isBusy = false;
        enemy.isAttacking = false;
    }

    #region Animation Events Called via AnimationTriggers
    public void EnableComboWindow() => canCombo = true;
    public void DisableComboWindow() => canCombo = false;
    public void OnAttackComplete() => attackFinished = true; //Debug.Log("[Combo] OnAttackComplete() 호출됨 → 공격 종료 처리");
    #endregion
}
