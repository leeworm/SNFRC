using UnityEngine;

public class PlayerAttackState1 : PlayerState
{
    public PlayerAttackState1(Player player, PlayerStateMachine stateMachine, string animBoolName)
        : base(player, stateMachine, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.5f;
        player.anim.Play("Attack1");
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
        /*else if (Input.GetKeyDown(KeyCode.X)) // 다음 공격
            stateMachine.ChangeState(player.attackState2)*/;
    }
}