using UnityEngine;

public class DH_PlayerSexyJutsuState : DH_PlayerState
{
    public DH_PlayerSexyJutsuState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DH_ScreenEffectManager.Instance.PlayEffectWithBlackScreen(fadeIn: 1f, hold: 1.8f, fadeOut: 1f);
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
