using UnityEngine;

public class PlayerSexyJutsuState : PlayerState
{
    public PlayerSexyJutsuState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ScreenEffectManager.Instance.PlayEffectWithBlackScreen(fadeIn: 1f, hold: 1.8f, fadeOut: 1f);
        player.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
