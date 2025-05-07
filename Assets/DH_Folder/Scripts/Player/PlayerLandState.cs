using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        player.anim.SetTrigger("Land"); // "Land"
    }


    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();


        if (Input.GetKeyDown(KeyCode.X) && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


    public override void Exit()
    {
        base.Exit();
        triggerCalled = false;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);
        if (yInput < 0)
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
        if (yInput > 0)
            stateMachine.ChangeState(player.jumpState);
        else
            stateMachine.ChangeState(player.idleState);
    }
}
