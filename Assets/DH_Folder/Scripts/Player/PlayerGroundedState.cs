using UnityEngine;

public abstract class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount = player.maxJumpCount;
        player.dashCommandDetector.Reset();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetButtonDown("Jump") && player.currentJumpCount > 0)
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (yInput < 0)
        {
            stateMachine.ChangeState(new PlayerCrouchState(player, stateMachine, "Crouch"));
            return;
        }
    }
}
