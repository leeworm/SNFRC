using UnityEngine;

public class BassIdleState : IEnemyState
{
    private Enemy_Bass bass;

    public BassIdleState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("Idle",true);
    }

    public void Update()
    {
        // 아무 행동 없음 (AI에서 자동 전환됨)
    }

    public void Exit() 
    {
        bass.animator.SetBool("Idle", false);
    }

    public void AnimationFinishTrigger() { }
}
