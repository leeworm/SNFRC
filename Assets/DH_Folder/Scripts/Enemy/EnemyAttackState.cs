using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        // 공격 애니메이션을 재생합니다.
        enemy.anim.SetTrigger("attack");
    }
    public override void Update()
    {
        base.Update();
        // 공격 로직을 여기에 추가합니다.
        // 예를 들어, 적이 플레이어에게 공격하는 로직을 구현할 수 있습니다.
    }
    public override void Exit()
    {
        base.Exit();
        // 공격 상태에서 나올 때 필요한 로직을 여기에 추가합니다.
    }
}