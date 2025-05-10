using UnityEngine;

public class JH_PlayerFBJumpAttackState : JH_PlayerState
{
    public JH_PlayerFBJumpAttackState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        int attackIndex = Player.LastAttackIndex;
        Player.animator.SetInteger("F/BJumpAttackIndex", attackIndex);

    }

    public override void Exit()
    {
        base.Exit();
        Player.animator.SetInteger("F/BJumpAttackIndex", 0);
    }

    public override void Update()
    {
        base.Update();
        if (Player.IsGroundDetected())
            StateMachine.ChangeState(Player.IdleState);

    }
}
