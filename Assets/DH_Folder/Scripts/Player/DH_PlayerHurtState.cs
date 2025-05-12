using UnityEngine;

public class DH_PlayerHurtState : DH_PlayerState
{
    private bool isKnockbacking = false;

    public DH_PlayerHurtState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        Debug.Log("Hurt State Entered");
        base.Enter();
        player.isHurting = true;
        player.anim.SetBool("Hurt", true); // 이 코드가 있어야 함
    }

    public override void Update()
    {
        base.Update();

        if (player.lastKnockback.y > 0.1f && !player.isGrounded)
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
        player.isHurting = false;
        player.anim.SetBool("Hurt", false);
    }
}
