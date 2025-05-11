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
        // 점프 카운트가 최대치보다 적으면 점프
        if (bass.jumpCount < bass.maxJumps)
        {
            bass.animator.SetTrigger("Jump");  // 점프 애니메이션 실행

            // 점프 물리적 속도 설정 (X축은 현재 속도를 유지, Y축은 위로 점프)
            rb.linearVelocity = new Vector2(bass.linearVelocityX, 8f);  // X축은 현재 속도, Y축은 점프 높이
            bass.jumpCount++;
        }
        else
        {
            // 점프가 끝난 후, 이동 상태로 돌아가기
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
        bass.animator.ResetTrigger("Jump");
    }

    public void AnimationFinishTrigger()
    {
        // 애니메이션 종료 시 추가 처리가 필요한 경우 여기에 구현
    }
}
