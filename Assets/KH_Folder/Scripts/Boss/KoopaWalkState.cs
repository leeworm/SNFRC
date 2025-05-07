using UnityEngine;

public class KoopaWalkState : KoopaState
{
    private Transform oldPlayerTransform;

    public KoopaWalkState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        oldPlayerTransform = koopa.playerTransform; // 플레이어의 위치를 저장합니다.
        if(koopa.transform.position.x > oldPlayerTransform.position.x) // 좌우 플립
        {
            koopa.SetVelocity(-koopa.moveSpeed,0); // 왼쪽으로 이동
        }
        else if(koopa.transform.position.x < oldPlayerTransform.position.x)
        {
            koopa.SetVelocity(koopa.moveSpeed,0); // 오른쪽으로 이동
        }
        

        stateTimer = 3f;
    }

    public override void Update()
    {
        base.Update();

        if(koopa.IsWallDetected()) // 벽에 부딪히면 방향 전환
        {
            koopa.Flip();
            koopa.SetVelocity(koopa.rb.linearVelocity.x * -1,0);
        }
        
        if(stateTimer <= 0)
        {
            // 보스 패턴 시작
            koopa.stateMachine.ChangeState(koopa.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        koopa.SetZeroVelocity(); // 이동 멈춤
    }
}
