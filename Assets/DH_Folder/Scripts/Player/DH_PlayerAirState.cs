using UnityEngine;

public class DH_PlayerAirState : DH_PlayerState
{
    private float currentXVelocity;

    public DH_PlayerAirState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        currentXVelocity = player.lastXVelocity; // 직전 속도 유지
        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
        player.commandDetectorEnabled = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Z) && !(player.currentState is DH_PlayerAirDefenseState) && !(player.currentState is DH_PlayerAirAttackState))
        {
            stateMachine.ChangeState(player.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && player.currentJumpCount > 0 && !(player.currentState is DH_PlayerAirDefenseState))
        {
            Debug.Log("에어에서 점프로 전이");
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (Input.GetKey(KeyCode.S) && !player.isBlocking)
        {
            stateMachine.ChangeState(player.airDefenseState);
            return;
        }

        if (player.IsGrounded())
        {
            stateMachine.ChangeState(player.landState);
            return;
        }

        if (xInput != 0)
            player.SetVelocity(currentXVelocity * xInput, rb.linearVelocity.y);
        else
        {
            currentXVelocity = 0f;
        }

        player.SetVelocity(currentXVelocity, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
    }


    protected bool IsGrounded()
    {
        return player.IsGroundDetected();
    }
}
