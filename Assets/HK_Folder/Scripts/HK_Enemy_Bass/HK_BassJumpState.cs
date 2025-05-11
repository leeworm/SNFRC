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
            bass.animator.SetTrigger("Jump");

            rb.linearVelocity = new Vector2(bass.linearVelocityX, 8f); // linearVelocity → velocity로 수정
            bass.jumpCount++;
        }
        else
        {
            bass.stateMachine.ChangeState(new HK_BassMoveState(bass));
        }
    }


    public void Update()
    {
        // 점프 후 Y축 속도가 0 이하로 떨어지면 이동 상태로 돌아감
        if (rb.linearVelocityY <= 0)  // Y축 속도가 0 이하일 때, 즉 점프가 끝날 때
        {
            bass.stateMachine.ChangeState(new HK_BassMoveState(bass));  // 이동 상태로 전환
        }
    }

    public void Exit()
    {
        // 트리거는 자동으로 리셋되므로 여기선 별도 처리 필요 없음
    }

    public void AnimationFinishTrigger()
    {
        // 애니메이션 종료 시 추가 처리가 필요한 경우 여기에 구현
    }
}
