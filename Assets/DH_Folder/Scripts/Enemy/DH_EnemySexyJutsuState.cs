using UnityEngine;

public class DH_EnemySexyJutsuState : DH_EnemyState
{
    public DH_EnemySexyJutsuState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DH_ScreenEffectManager.Instance.PlayEffectWithBlackScreen(fadeIn: 1f, hold: 1.8f, fadeOut: 1f);
        enemy.isBusy = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isBusy = false;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }
}
