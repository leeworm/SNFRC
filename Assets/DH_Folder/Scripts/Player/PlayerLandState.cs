using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.anim.ResetTrigger("Land");
        player.anim.SetTrigger("Land");
    }


    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump"))
            return;
        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.ResetTrigger("Land");
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);
        else
            stateMachine.ChangeState(player.idleState);
    }
}
