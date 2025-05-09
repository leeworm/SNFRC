using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.3f;
        player.anim.Play("Player_Hit");
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
    }
}