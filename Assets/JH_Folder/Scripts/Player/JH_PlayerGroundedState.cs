using UnityEngine;

public class JH_PlayerGroundedState : JH_PlayerState
{
    private float lastUpPressTime = -1f;
    private float commandInputWindow = 0.1f;
    private bool waitingForSideInput = false;

    public JH_PlayerGroundedState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool) : base(_Player, _StateMachine, _AnimBool)
    {
    }

    public override void Enter()
    {
        base.Enter();
        waitingForSideInput = false;
        lastUpPressTime = -1f;
    }

    public override void Update()
    {
        base.Update();
        bool isGrounded = Player.IsGroundDetected();

        if (yInput > 0 && !waitingForSideInput && isGrounded)
        {
            waitingForSideInput = true; 
            lastUpPressTime = Time.time; 
            
        }

        if (waitingForSideInput)
        {
            if (xInput > 0)
            {
                StateMachine.ChangeState(Player.ForwardJumpState);
                waitingForSideInput = false; 
                return;
            }

            if (xInput < 0)
            {
                StateMachine.ChangeState(Player.BackJumpState);
                waitingForSideInput = false;
                return;
            }


            if (Time.time - lastUpPressTime > commandInputWindow)
            {
                StateMachine.ChangeState(Player.JumpState);
                waitingForSideInput = false;
                return;
            }
        }

        if (yInput < 0 && Player.IsGroundDetected())
        {
            StateMachine.ChangeState(Player.CrouchState);
             return;
        }

    }

    public override void Exit()
    {
        base.Exit();
        waitingForSideInput = false;
    }
}