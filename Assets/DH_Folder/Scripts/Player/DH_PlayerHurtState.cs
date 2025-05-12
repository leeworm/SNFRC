using UnityEngine;

public class DH_PlayerHurtState : DH_PlayerState
{
    private bool playedKnockbackAnim = false;

    public DH_PlayerHurtState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.SetVelocity(player.lastKnockback.x, player.lastKnockback.y);

        // 넉백 방향에 따라 애니메이션 선택
        if (Mathf.Abs(player.lastKnockback.x) > 0.1f)
        {
            player.anim.Play("Knockback");
            playedKnockbackAnim = true;
        }
        else
        {
            player.anim.Play("Hurt");
        }
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && !player.IsGrounded())
        {
            player.anim.Play("Knockback");
        }

        if (player.isGrounded && playedKnockbackAnim)
        {
            stateMachine.ChangeState(player.knockdownState);
        }
        else if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
    }
}
