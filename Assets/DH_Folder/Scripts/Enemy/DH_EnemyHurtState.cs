using UnityEngine;

public class DH_EnemyHurtState : DH_EnemyState
{
    private bool playedKnockbackAnim = false;

    public DH_EnemyHurtState(DH_Enemy _enemy, DH_EnemyStateMachine stateMachine, string animBoolName)
        : base(_enemy, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.isBusy = true;
        enemy.SetVelocity(enemy.lastKnockback.x, enemy.lastKnockback.y);

        // 넉백 방향에 따라 애니메이션 선택
        if (Mathf.Abs(enemy.lastKnockback.x) > 0.1f)
        {
            enemy.anim.Play("Knockback");
            playedKnockbackAnim = true;
        }
        else
        {
            enemy.anim.Play("Hurt");
        }
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && !enemy.IsGrounded())
        {
            enemy.anim.Play("Knockback");
        }

        if (enemy.isGrounded && playedKnockbackAnim)
        {
            stateMachine.ChangeState(enemy.knockdownState);
        }
        else if (enemy.isGrounded)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }
}
