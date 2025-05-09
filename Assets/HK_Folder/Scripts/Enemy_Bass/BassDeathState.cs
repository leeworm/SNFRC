
using UnityEngine;

public class BassDeathState : IEnemyState
{
    private Enemy_Bass bass;
    private float deathTimer = 2f;

    public BassDeathState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("Die");
        bass.SetVelocity(Vector2.zero); // 움직임 정지

        // 기타 필요 요소: 콜라이더 비활성화 등
        if (bass.GetComponent<Collider2D>()) bass.GetComponent<Collider2D>().enabled = false;
    }

    public void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            GameObject.Destroy(bass.gameObject); // 일정 시간 후 제거
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
