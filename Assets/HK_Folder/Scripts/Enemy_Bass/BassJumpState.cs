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
            rb.linearVelocity = new Vector2(rb.linearVelocityX, 8f);  // ������
            bass.jumpCount++;
        }

        bass.stateMachine.ChangeState(new BassMoveState(bass));
    }
    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼� ������ �� ������ �ڵ�
    }
    public void Update() { }

    public void Exit() { }
}
