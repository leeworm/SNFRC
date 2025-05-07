using UnityEngine;

public class ProtoManJumpState : IEnemyState
{
    private Enemy_ProtoMan protoMan;

    public ProtoManJumpState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
    }

    public void Enter()
    {
        // 대쉬 슬래시 공격 초기화
    }

    public void Exit()
    {
        // 상태 종료 시 처리
    }

    public void Update()
    {
        // 상태 업데이트 로직
    }

    public void AnimationFinishTrigger()
    {
        // 애니메이션이 끝났을 때 실행되는 로직
    }
}

