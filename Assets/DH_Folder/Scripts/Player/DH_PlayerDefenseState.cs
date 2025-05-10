using UnityEngine;

public class DH_PlayerDefenseState : DH_PlayerGroundedState
{
    public DH_PlayerDefenseState(DH_Player player, DH_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.isBlocking = true;
        player.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        player.isBlocking = false;
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}
