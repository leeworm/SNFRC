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
            rb.linearVelocity = new Vector2(bass.linearVelocityX, 8f); // 수직 점프력
            bass.jumpCount++;
        }

        bass.stateMachine.ChangeState(new BassMoveState(bass));
    }

    public void Update() { }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
