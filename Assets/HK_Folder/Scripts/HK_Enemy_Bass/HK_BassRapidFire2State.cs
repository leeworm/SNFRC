using UnityEngine;

public class HK_BassRapidFire2State : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private float fireCooldown = 0.3f; // 발사 쿨타임 (초)
    private float fireTimer = 0f; // 쿨타임을 측정하는 타이머
    private int shotCount = 0; // 발사한 총알 수
    private int maxShots = 4; // 최대 발사 횟수

    public HK_BassRapidFire2State(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("RapidFire2"); // RapidFire2 애니메이션 시작
        shotCount = 0; // 발사된 총알 수 초기화
        fireTimer = 0f; // 타이머 초기화
    }

    public void Update()
    {
        fireTimer += Time.deltaTime; // 매 프레임마다 타이머 증가

        // 쿨타임이 지나고, 최대 발사 횟수에 도달하지 않으면 발사
        if (fireTimer >= fireCooldown && shotCount < maxShots)
        {
            bass.FireRapidShot2(); // RapidFire2 발사
            fireTimer = 0f; // 타이머 리셋
            shotCount++; // 발사 횟수 증가
        }

        // 최대 발사 횟수에 도달하면 상태 전환
        if (shotCount >= maxShots)
        {
            bass.stateMachine.ChangeState(new HK_BassMoveState(bass)); // 이동 상태로 전환
        }
    }

    public void Exit()
    {
        bass.animator.ResetTrigger("RapidFire2"); // RapidFire2 애니메이션 리셋
    }

    public void AnimationFinishTrigger()
    {
        // 애니메이션이 끝났을 때 추가적인 처리가 필요할 경우 여기에 작성
    }
}
