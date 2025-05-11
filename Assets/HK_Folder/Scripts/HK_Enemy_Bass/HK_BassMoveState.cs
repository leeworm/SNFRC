using UnityEngine;

public class HK_BassMoveState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private float speed = 2.5f;

    public HK_BassMoveState(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("isMoving", true);  // 이동 중 애니메이션 설정
    }

    public void Update()
    {
        if (bass.player == null) return;

        // 플레이어를 향해 이동
        Vector2 direction = (bass.player.position - bass.transform.position).normalized;
        bass.transform.position += (Vector3)(new Vector2(direction.x, 0) * speed * Time.deltaTime);

        // 플레이어와의 거리 계산
        float distance = Vector2.Distance(bass.transform.position, bass.player.position);
        if (distance <= bass.attackRange)  // 공격 범위 내에 들어가면
        {
            // 공격 범위에 들어가면 랜덤한 rapidFireType을 선택하여 RapidFireState로 전환
            int rapidFireType = Random.Range(1, 3);  // RapidFire1 또는 RapidFire2 선택
            bass.stateMachine.ChangeState(new HK_BassRapidFireState(bass, rapidFireType));  // rapidFireType 전달
        }
    }

    public void Exit()
    {
        bass.animator.SetBool("isMoving", false);  // 이동 종료 애니메이션 설정
    }

    public void AnimationFinishTrigger() { }
}
