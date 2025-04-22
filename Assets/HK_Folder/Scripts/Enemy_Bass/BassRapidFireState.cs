using UnityEngine;

public class BassRapidFireState : IEnemyState
{
    private Enemy_Bass bass;
    private float fireRate = 0.2f;
    private float fireTimer;
    private int shotsFired;
    private int maxShots = 5;

    public BassRapidFireState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        fireTimer = 0f;
        shotsFired = 0;
    }

    public void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate && shotsFired < maxShots)
        {
            fireTimer = 0f;
            shotsFired++;

            GameObject shot = GameObject.Instantiate(bass.rapidShotPrefab, bass.firePoint.position, Quaternion.identity);
            Vector2 direction = bass.player.position - bass.firePoint.position;
            shot.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * 12f;
        }

        if (shotsFired >= maxShots)
        {
            bass.stateMachine.ChangeState(new BassJumpState(bass));
        }
    }
    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
    public void Exit() { }
}
