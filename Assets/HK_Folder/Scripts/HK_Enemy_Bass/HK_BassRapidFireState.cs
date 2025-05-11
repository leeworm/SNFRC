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
                fireRate = 0.2f;
                maxShots = 5;
                animationTrigger = "RapidFire1";
                break;
            case 2:
                fireRate = 0.15f;
                maxShots = 7;
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

        if (fireTimer >= fireRate && shotsFired < maxShots)
        {
            fireTimer = 0f;
            shotsFired++;

            Fire();

            if (shotsFired >= maxShots)
            {
                bass.stateMachine.ChangeState(new HK_BassMoveState(bass));
            }
        }
    }

    private void Fire()
    {
        // 타입에 따라 발사 방식 변경 가능
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

