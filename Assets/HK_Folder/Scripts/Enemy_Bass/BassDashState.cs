using System.Collections;
using UnityEngine;

public class BassDashState : IEnemyState
{
    private Enemy_Bass bass;
    private Rigidbody2D rb;

    public BassDashState(Enemy_Bass bass)
    {
        this.bass = bass;
        rb = bass.GetComponent<Rigidbody2D>();
    }

    public void Enter()
    {
        Vector2 dashDirection = (bass.player.position - bass.transform.position).normalized;
        rb.linearVelocity = dashDirection * 10f;

        
        bass.StartCoroutine(EndDash());
    }

    public void Update() { }

    public void Exit() { }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(0.3f);
        bass.stateMachine.ChangeState(new BassMoveState(bass));
    }
    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
}
