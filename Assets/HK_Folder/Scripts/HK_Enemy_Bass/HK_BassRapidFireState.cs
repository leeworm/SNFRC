using UnityEngine;

public class HK_BassRapidFireState : HK_IEnemyState
{
    private readonly HK_Enemy_Bass bass;
    private readonly float fireRate;
    private readonly int maxShots;
    private readonly string animationTrigger;
    private readonly int rapidFireType;

    private float fireTimer;
    private int shotsFired;

    public HK_BassRapidFireState(HK_Enemy_Bass bass, int rapidFireType)
    {
        this.bass = bass;
        this.rapidFireType = rapidFireType;

        // 타입별 설정
        switch (rapidFireType)
        {
            case 1:
                fireRate = 0.2f;  // 공격 간격
                maxShots = 2;     // 최대 발사 횟수
                animationTrigger = "RapidFire1";
                break;
            case 2:
                fireRate = 0.15f; // 공격 간격
                maxShots = 3;     // 최대 발사 횟수
                animationTrigger = "RapidFire2";
                break;
            default:
                Debug.LogWarning("Invalid RapidFireType. Defaulting to Type 1.");
                fireRate = 0.2f;
                maxShots = 5;
                animationTrigger = "RapidFire1";
                break;
        }
    }

    public void Enter()
    {
        fireTimer = 0f;
        shotsFired = 0;
        bass.animator.SetTrigger(animationTrigger);
    }

    public void Update()
    {
        fireTimer += Time.deltaTime;

        // fireRate에 맞춰 공격하고, 최대 공격 횟수를 초과하지 않도록 제한
        if (fireTimer >= fireRate && shotsFired < maxShots)
        {
            fireTimer = 0f;
            shotsFired++;

            Fire();

            // 최대 발사 횟수에 도달하면 상태 전환
            if (shotsFired >= maxShots)
            {
                bass.stateMachine.ChangeState(new HK_BassMoveState(bass));
            }
        }
    }

    private void Fire()
    {
        // 공격 타입에 맞는 발사 메서드 호출
        switch (rapidFireType)
        {
            case 1:
                bass.FireRapidShot1();
                break;
            case 2:
                bass.FireRapidShot2();
                break;
        }
    }

    public void AnimationFinishTrigger()
    {
        if (shotsFired >= maxShots)
        {
            bass.stateMachine.ChangeState(new HK_BassMoveState(bass));
        }
    }

    public void Exit()
    {
        bass.animator.ResetTrigger(animationTrigger);
    }
}
