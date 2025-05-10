public class DH_PlayerUppercutState : DH_PlayerGroundedState
{
    public DH_PlayerUppercutState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.linearVelocity.y);
        player.isBusy = true;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
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
