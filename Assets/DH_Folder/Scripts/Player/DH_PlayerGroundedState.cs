using UnityEngine;

public abstract class DH_PlayerGroundedState : DH_PlayerState
{

    public DH_PlayerGroundedState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        player.currentJumpCount = player.maxJumpCount;
        player.CommandDetector.Reset();
    }
    public override void Update()
    {
        base.Update();

        if (player.isLanding == true && !player.isBusy)
        {
            stateMachine.ChangeState(player.landState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && yInput < 0)
        {
            stateMachine.ChangeState(player.crouchState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Z) && !player.isBusy && yInput > 0.1f)
        {
            player.primaryAttackComboCounter = 0;
            stateMachine.ChangeState(new DH_PlayerUppercutState(player, stateMachine, "Uppercut"));
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && !player.isBusy && !(yInput > 0.1f) && !(yInput < 0))
        {
            stateMachine.ChangeState(player.primaryAttack);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.X)
            && player.currentJumpCount > 0
            && !player.isBusy
            && !(stateMachine.currentState is DH_PlayerCrouchState)
            && !(stateMachine.currentState is DH_PlayerBackstepState)
            && !(stateMachine.currentState is DH_PlayerPrimaryAttackState))
        {
            stateMachine.ChangeState(new DH_PlayerJumpState(player, stateMachine, "Jump", player.lastXVelocity));
            return;
        }

        if (Input.GetKey(KeyCode.S) && !player.isBlocking)
        {
            stateMachine.ChangeState(new DH_PlayerDefenseState(player, stateMachine, "Block"));
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {            
            stateMachine.ChangeState(new DH_PlayerSexyJutsuState(player, stateMachine, "Skill1"));
            return;
        }
    }
}
