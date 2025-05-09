using UnityEngine;

public class PlayerDefenseState : PlayerGroundedState
{
    public PlayerDefenseState(Player player, PlayerStateMachine stateMachine, string animBoolName)
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
