using UnityEngine;

public class BassKickState : IEnemyState
{
    private Enemy_Bass bass;
    private bool hasKicked = false;

    public BassKickState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        hasKicked = false;
        bass.animator.SetTrigger("Kick");
    }

    public void Update()
    {
        if (!hasKicked)
        {
            // 여기에 실제 데미지 처리나 히트박스 활성화 등을 넣을 수 있음
            // 예: 근접 히트박스를 잠깐 활성화하거나 trigger 처리

            hasKicked = true;
        }
    }

    public void Exit()
    {
        // 필요시 히트박스 비활성화 등
    }

    public void AnimationFinishTrigger()
    {
        bass.stateMachine.ChangeState(new BassIdleState(bass));
    }
}
