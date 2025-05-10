using UnityEngine;

public class JH_PlayerCrouchAttackState : JH_PlayerState
{
    public JH_PlayerCrouchAttackState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        int attackIndex = Player.LastAttackIndex;
        Player.animator.SetInteger("CrouchAttackIndex", attackIndex);
        Player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
        Player.animator.SetInteger("CrouchAttackIndex", 0);
    }

}
