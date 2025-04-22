using UnityEngine;

public class BassIdleState : IEnemyState
{
    private Enemy_Bass bass;
    private float timer;
    private float waitTime;

    public BassIdleState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        waitTime = Random.Range(0.5f, 1.5f);
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            bass.stateMachine.ChangeState(new BassMoveState(bass));
        }
    }
    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
    public void Exit() { }
}
