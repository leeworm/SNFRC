using UnityEngine;

public class DH_PlayerHurtState : DH_PlayerGroundedState
{
    private bool isKnockbacking = false;

    public DH_PlayerHurtState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.isHurting = true;
        player.anim.SetBool("Hurt", true);
        player.SetVelocity(player.lastKnockback.x, player.lastKnockback.y);        
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y > 0.1f && !player.IsGrounded())
        {
            player.anim.SetBool("Hurt", false);
            player.anim.SetBool("Knockback", true);
            isKnockbacking = true;
        }

        if (player.isGrounded && isKnockbacking)
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
        player.isHurting = false;
        player.anim.SetBool("Hurt", false);
    }
}
