using UnityEngine;

public class DH_PlayerSexyJutsuState : DH_PlayerState
{
    public Transform enemy;
    public DH_PlayerSexyJutsuState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DH_ScreenEffectManager.Instance.PlayEffectWithBlackScreen(fadeIn: 1f, hold: 1.8f, fadeOut: 1f);
        player.isBusy = true;
        enemy = GameObject.FindGameObjectWithTag("Enemy")?.transform;
        //enemy 찾았다면 enemy 잠시 아이들 상태로 5초간 고정해버리기 멈추기(BusyFor 코루틴 사용)
        if (enemy != null)
        {
            Debug.Log("Enemy found! 적은 헤롱헤롱 거린다.");
            enemy.GetComponent<DH_Enemy>().isBusy = true;
            enemy.GetComponent<DH_Enemy>().stateMachine.ChangeState(enemy.GetComponent<DH_Enemy>().idleState);
            enemy.GetComponent<DH_Enemy>().StartCoroutine(enemy.GetComponent<DH_Enemy>().BusyFor(10f));
        }

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
