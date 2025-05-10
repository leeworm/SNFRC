using UnityEngine;

public class BassMoveState : IEnemyState
{
    private Enemy_Bass bass;
    private float speed = 2.5f;

    public BassMoveState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("isMoving", true);  // 이동 애니메이션 시작
    }

    public void Update()
    {
        if (bass.player == null) return;

        // 플레이어와의 방향을 계산하여 이동
        Vector2 direction = (bass.player.position - bass.transform.position).normalized;
        bass.transform.position += (Vector3)(new Vector2(direction.x, 0) * speed * Time.deltaTime);

        // 만약 플레이어와의 거리가 충분히 가까워지면 이동을 멈추고 다른 상태로 전환할 수 있음
        float distance = Vector2.Distance(bass.transform.position, bass.player.position);
        if (distance <= bass.attackRange)  // 일정 범위 이내로 접근했을 경우
        {
            bass.stateMachine.ChangeState(new BassRapidFireState(bass));  // 예시로 공격 상태로 전환
        }
    }

    public void Exit()
    {
        bass.animator.SetBool("isMoving", false);  // 이동 애니메이션 종료
    }

    public void AnimationFinishTrigger() { }
}
