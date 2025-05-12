using UnityEngine;

public class DH_EnemyDeadState : DH_EnemyState
{
    public DH_EnemyDeadState(DH_Enemy _enemy, DH_EnemyStateMachine _stateMachine, string _animBoolName)
        : base(_enemy, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        enemy.MakeTransparent(true); // 필요하면 사라지는 연출
        enemy.Die(); // Entity 클래스에서 오버라이드 가능
    }

    public override void Update()
    {
        base.Update();
        // 입력 무시. 아무것도 안 함.
    }
}
