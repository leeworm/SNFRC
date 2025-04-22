using UnityEngine;

public class ProtoManIdleState : IEnemyState
{
    private Enemy_ProtoMan protoMan;
    private float idleTime;
    private float timer;

    public ProtoManIdleState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        idleTime = Random.Range(1f, 2f);
        timer = 0f;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= idleTime)
        {
            protoMan.stateMachine.ChangeState(new ProtoManMoveState(protoMan));
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        // 애니메이션 끝났을 때 실행할 코드
    }
}