using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        // 애니메이션이 끝났을 때 호출되는 메서드입니다.
        // 필요한 경우 추가 로직을 여기에 작성할 수 있습니다.
    }
    public override void Enter()
    {
        base.Enter();
        // enemy.SetVelocity(1f, 0f); // 적의 속도를 설정합니다.
    }
    public override void Update()
    {
        base.Update();
        // 적의 이동 로직을 여기에 추가합니다.
        // 예를 들어, 적이 플레이어를 추적하도록 할 수 있습니다.
    }
    public override void Exit()
    {
        base.Exit();
        // enemy.SetVelocity(0f, 0f); // 적의 속도를 0으로 설정합니다.
    }
}