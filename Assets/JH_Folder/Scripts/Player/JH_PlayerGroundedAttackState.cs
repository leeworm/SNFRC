using UnityEngine;

public class JH_PlayerGroundedAttackState : JH_PlayerState
{
    public JH_PlayerGroundedAttackState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        int attackIndex = Player.LastAttackIndex;
        Player.animator.SetInteger("GroundedAttackIndex", attackIndex);
        Player.SetVelocity(0 ,0);

    }

    public override void Update()
    {
        base.Update();
        
    }

    public override void AnimationFinishTrigger()
    {

        StateMachine.ChangeState(Player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        Player.animator.SetInteger("GroundedAttackIndex", 0);
    }

    
}
