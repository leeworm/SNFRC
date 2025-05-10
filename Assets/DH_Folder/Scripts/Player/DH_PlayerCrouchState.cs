using UnityEngine;

public class DH_PlayerCrouchState : DH_PlayerGroundedState
{
    private bool substitutionWindowOpen = false;

    public DH_PlayerCrouchState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;

        player.SetVelocity(0, rb.linearVelocity.y);
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

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && substitutionWindowOpen && player.canSubstitute())
        {
            stateMachine.ChangeState(player.substituteState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        player.col.size = player.originalColliderSize;
        player.col.offset = player.originalColliderOffset;
    }

    public void OpenSubstitutionWindow()
    {
        substitutionWindowOpen = true;
    }

    public void CloseSubstitutionWindow()
    {
        substitutionWindowOpen = false;
    }

}
