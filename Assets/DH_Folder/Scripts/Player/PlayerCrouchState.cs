using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (yInput >= 0)
        {
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
        }

        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(new PlayerSubstituteState(player, stateMachine, "Substitute"));
        }
    }
}
