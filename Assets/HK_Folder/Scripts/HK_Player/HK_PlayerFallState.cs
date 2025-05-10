using UnityEngine;

public class HK_PlayerFallState : HK_PlayerState
{
    public HK_PlayerFallState(HK_Player player, HK_PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
    }
}