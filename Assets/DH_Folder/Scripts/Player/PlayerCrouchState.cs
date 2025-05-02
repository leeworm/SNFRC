using UnityEngine;

public class PlayerCrouchState : PlayerGroundedState
{
    public PlayerCrouchState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
        Vector2 newSize = new Vector2(player.originalColliderSize.x, player.originalColliderSize.y * 0.5f);
        player.col.size = newSize;

        Vector2 newOffset = new Vector2(
        player.originalColliderOffset.x,
        player.originalColliderOffset.y - (player.originalColliderSize.y - newSize.y) / 2f);
        player.col.offset = newOffset;
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
    public override void Exit()
    {
        base.Exit();

        player.col.size = player.originalColliderSize;
        player.col.offset = player.originalColliderOffset;
    }
}
