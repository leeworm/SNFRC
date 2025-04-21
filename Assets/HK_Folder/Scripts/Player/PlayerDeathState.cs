using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.anim.Play("Death");
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
        // 게임 오버 처리 가능
    }
}