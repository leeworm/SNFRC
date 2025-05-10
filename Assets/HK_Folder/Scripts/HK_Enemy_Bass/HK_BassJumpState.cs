using UnityEngine;

public class HK_BassJumpState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private Rigidbody2D rb;

    public HK_BassJumpState(HK_Enemy_Bass bass)
    {
        this.bass = bass;
        rb = bass.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        if (bass.jumpCount < bass.maxJumps)
        {
            rb.linearVelocity = new Vector2(bass.linearVelocityX, 8f); // ���� ������
            bass.jumpCount++;
        }

        bass.stateMachine.ChangeState(new HK_BassMoveState(bass));
    }

    public void Update() { }

    public void Exit() { }

    public void AnimationFinishTrigger() { }
}
