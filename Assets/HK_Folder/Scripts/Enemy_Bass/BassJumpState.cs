using UnityEngine;

public class BassJumpState : IEnemyState
{
    private Enemy_Bass bass;
    private Rigidbody2D rb;

    public BassJumpState(Enemy_Bass bass)
    {
        this.bass = bass;
        rb = bass.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        if (bass.jumpCount < bass.maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 8f);  // 점프력
            bass.jumpCount++;
        }

        bass.stateMachine.ChangeState(new BassMoveState(bass));
    }
    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
    public void Update() { }

    public void Exit() { }
}
