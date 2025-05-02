using UnityEngine;

public abstract class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    protected bool IsGrounded()
    {
        return player.IsGroundDetected();
    }
}
