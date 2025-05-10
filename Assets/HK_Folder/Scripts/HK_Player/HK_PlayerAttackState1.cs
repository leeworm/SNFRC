using UnityEngine;

public class HK_PlayerAttackState1 : HK_PlayerState
{
    public HK_PlayerAttackState1(HK_Player player, HK_PlayerStateMachine stateMachine, string animBoolName)
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
        /*else if (Input.GetKeyDown(KeyCode.X)) // ���� ����
            stateMachine.ChangeState(player.attackState2)*/;
    }
}