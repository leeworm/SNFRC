using UnityEngine;

public class KoopaAllDirFireState : KoopaState
{
    private float fireTimer;

    private float StartAngleIncrement;

    public KoopaAllDirFireState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 5f; // 상태 지속 시간
        fireTimer = 0.5f; // 발사 간격

        StartAngleIncrement = koopa.angleIncrement;
        koopa.rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        fireTimer -= Time.deltaTime;

        if(stateTimer <= 0)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        if(koopa.phaseState == PhaseState.Phase1)
        {
            GoPattern(new Vector3(0, 3, 0), 1.0f);
        }
        else if(koopa.phaseState == PhaseState.Phase2)
        {
            GoPattern(new Vector3(0, -195, 0), 0.5f);
        }
        else
        {
            Debug.Log("AllDirFireState : PhaseState Error");
        }
    }

    public override void Exit()
    {
        base.Exit();

        koopa.angleIncrement = StartAngleIncrement;
        koopa.rb.gravityScale = 1;
    }

    public void GoPattern(Vector3 _midPos, float _fireDelay)
    {
        if(koopa.transform.position == _midPos)
        {
            if(fireTimer <= 0)
            {
                koopa.AllDirFire(); // 모든 방향으로 불꽃 발사
                fireTimer = _fireDelay; // 발사 딜레이
            }
        }
        else
        {
            koopa.transform.position = Vector3.MoveTowards(koopa.transform.position, _midPos, koopa.moveSpeed * Time.deltaTime);
        }
    }
}
