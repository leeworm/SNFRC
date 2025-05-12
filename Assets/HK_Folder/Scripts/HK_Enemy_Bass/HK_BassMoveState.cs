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

        // AI가 지정한 방향대로 이동 (Y축 고정)
        Vector2 move = new Vector2(bass.moveDirection.x, 0f);
        bass.transform.position += (Vector3)(move * speed * Time.deltaTime);

        // 플레이어와의 거리 계산
        float distance = Vector2.Distance(bass.transform.position, bass.player.position);
        if (distance <= bass.attackRange)
        {
            int rapidFireType = Random.Range(1, 3);  // RapidFire1 또는 RapidFire2
            bass.stateMachine.ChangeState(new HK_BassRapidFireState(bass, rapidFireType));
        }
    }

    public void Exit()
    {
        bass.animator.SetBool("isMoving", false);  // 이동 종료 애니메이션 설정
    }

    public void AnimationFinishTrigger() { }
}
