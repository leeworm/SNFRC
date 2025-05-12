using UnityEngine;

public class KoopaWalkState : KoopaState
{
    private Transform oldPlayerTransform;

    int patternRandomNum = 0; // 패턴 랜덤 넘버

    public KoopaWalkState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        oldPlayerTransform = koopa.playerTransform; // 플레이어의 위치를 저장합니다.
        if(koopa.transform.position.x > oldPlayerTransform.position.x) 
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

        if(koopa.healthPoint <= 200 && koopa.phaseState == PhaseState.Phase1) // 체력이 200 이하일 때
        {
            koopa.phaseState = PhaseState.PhaseChange;
            koopa.healthPoint = 1000; // 체력 초기화
            koopa.koopaHpBar.SetHpBar(koopa.healthPoint); // 체력바 초기화
            koopa.stateMachine.ChangeState(koopa.jumpAttackState);
        }

        if(koopa.IsWallDetected()) // 벽에 부딪히면 방향 전환
        {
            koopa.Flip();
            koopa.SetVelocity(koopa.rb.linearVelocity.x * -1,0);
        }
        
        if(stateTimer <= 0 && koopa.phaseState != PhaseState.PhaseChange)
        {
            Phase1_Pattern();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Phase1_Pattern()
    {
        if(koopa.phaseState == PhaseState.Phase1)
            patternRandomNum = Random.Range(0, 4);
        else if(koopa.phaseState == PhaseState.Phase2)
            patternRandomNum = Random.Range(4, 8);

        Debug.Log("패턴 번호 : " + patternRandomNum);

        if(patternRandomNum == 0) // 불꽃 발사 패턴
        {
            koopa.stateMachine.ChangeState(koopa.fireShotState);
        }
        else if(patternRandomNum == 1) // 점프 공격 패턴
        {
            koopa.stateMachine.ChangeState(koopa.jumpAttackState);
        }
        else if(patternRandomNum == 2) // 모든 방향으로 불꽃 발사 패턴
        {
            koopa.stateMachine.ChangeState(koopa.allDirFireState);
        }
        else if(patternRandomNum == 3) // 롤링 불꽃 발사 패턴
        {
            koopa.stateMachine.ChangeState(koopa.roundFireState);
        }
        else if(patternRandomNum == 4 || patternRandomNum == 5) // 스핀 공격 패턴
        {
            koopa.stateMachine.ChangeState(koopa.spinAttackState);
        }
        else if(patternRandomNum == 6 || patternRandomNum == 7) // 레이저 공격 패턴
        {
            koopa.stateMachine.ChangeState(koopa.laserShotState);
        }
    }

}
