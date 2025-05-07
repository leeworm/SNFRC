using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private bool isFloating = false;
    private float floatDuration = 3f;
    private float floatTimer;

    private float originalGravityScale;
    private float currentXVelocity;

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, float initialXVelocity)
        : base(_player, _stateMachine, _animBoolName)
    {
        currentXVelocity = initialXVelocity;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log($"[JumpState] 진입 전 currentJumpCount = {player.currentJumpCount}");

        player.anim.SetTrigger("Jump");

        if (player.currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(currentXVelocity, player.jumpForce);
            player.currentJumpCount--;
            Debug.Log($"[JumpState] 점프 실행! 남은 currentJumpCount = {player.currentJumpCount}");
        }
        else
        {
            Debug.LogWarning("[JumpState] currentJumpCount가 0이어서 점프 무시됨");
        }

        isFloating = false;
        floatTimer = 0f;

        // 원래 중력 저장
        originalGravityScale = rb.gravityScale;
    }

    public override void Update()
    {
        base.Update();

        float yVelocity = rb.linearVelocity.y;

        // 최고점 도달 직후 → 체공 시작
        if (!isFloating && yVelocity < 0.05f && yVelocity > -0.05f)
        {
            isFloating = true;
            floatTimer = floatDuration;

            // 중력을 완전히 제거해서 공중에 정지
            rb.gravityScale = 0f;
        }

        // 체공 중일 때
        if (isFloating)
        {
            floatTimer -= Time.deltaTime;

            if (floatTimer < 0f)
            {
                // 체공 끝 → 중력 원상복구
                isFloating = false;
                rb.gravityScale = originalGravityScale;
            }

            // AirState로 전이 안 하도록 여기서 return
            return;
        }

        // 체공이 끝나고 실제 낙하 시작되면 AirState로 전이
        if (!isFloating && rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        // 혹시 모르니 나갈 때 중력 복원 보장
        rb.gravityScale = originalGravityScale;
    }
}
