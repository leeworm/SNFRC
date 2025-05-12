using UnityEngine;

public class DH_PlayerJumpState : DH_PlayerState
{
    private float currentXVelocity;

    public DH_PlayerJumpState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName, float initialXVelocity)
        : base(_player, _stateMachine, _animBoolName)
    {
        currentXVelocity = initialXVelocity;
    }

    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        player.isJumping = true;

        currentXVelocity = rb.linearVelocity.x;
        
        if (player.currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(currentXVelocity, player.jumpForce);
            player.currentJumpCount--;
        }
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            float speedMagnitude = Mathf.Max(Mathf.Abs(currentXVelocity), player.moveSpeed);
            float xSpeed = speedMagnitude * xInput;

            // 속도가 0이면 → 기본 이동 속도로 덮어쓰기
            if (speedMagnitude == 0)
                speedMagnitude = player.moveSpeed;

            rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
            player.lastXVelocity = xSpeed;
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            stateMachine.ChangeState(player.airAttackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.X) && player.currentJumpCount > 0)
        {
            Debug.Log("점프에서 점프로 전이");
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        // 낙하 시작되면 AirState로 전이
        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        player.isJumping = false;
    }
}
