using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        player.MakeTransparent(true); // 필요하면 사라지는 연출
        player.Die(); // Entity 클래스에서 오버라이드 가능
    }

    public override void Update()
    {
        base.Update();
        // 입력 무시. 아무것도 안 함.
    }
}
