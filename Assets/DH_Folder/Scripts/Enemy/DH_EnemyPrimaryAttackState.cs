using UnityEngine;

public class DH_EnemyPrimaryAttackState : DH_EnemyState
{
    private int comboIndex = 0;
    private bool isComboQueued = false;

    public DH_EnemyPrimaryAttackState(DH_Enemy enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log($"[EnemyAttack] Enter - comboIndex: {comboIndex}");
        enemy.isAttacking = true;
        comboIndex = 0;
        isComboQueued = false;

        DoAttack();
    }

    public override void Update()
    {
        base.Update();

        // 다음 공격 입력 큐에 저장
        if (enemy.isAttackInput)
        {
            isComboQueued = true;
            enemy.isAttackInput = false;
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        Debug.Log($"[EnemyAttack] AnimationFinished - comboIndex: {comboIndex}, queued: {isComboQueued}");

        if (isComboQueued && comboIndex < 2)
        {
            comboIndex++;
            DoAttack();
            isComboQueued = false;
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    private void DoAttack()
    {
        enemy.anim.SetInteger("ComboCounter", comboIndex);
        enemy.anim.SetBool("Attack", true);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("[EnemyAttack] Exit");
        enemy.StartCoroutine("BusyFor", 0.1f);
        enemy.anim.SetBool("Attack", false);
        comboIndex = 0;
        isComboQueued = false;
    }

    // DH_EnemyAnimationTrigger.cs에 있는 함수
    public void ActivateHitbox(string hitboxName) => enemy.ActivateHitbox(hitboxName);
    public void DeactivateHitbox(string hitboxName) => enemy.DeactivateHitbox(hitboxName);

}
