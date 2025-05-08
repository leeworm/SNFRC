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

        if(koopa.transform.position == new Vector3(0, 3, 0))
        {
            if(fireTimer <= 0)
            {
                koopa.AllDirFire(); // 모든 방향으로 불꽃 발사
                fireTimer = 0.5f; // 발사 간격 초기화
            }
        }
        else{
            koopa.transform.position = Vector3.MoveTowards(koopa.transform.position, new Vector3(0, 3, 0), koopa.moveSpeed * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        base.Exit();

        koopa.angleIncrement = StartAngleIncrement;
        koopa.rb.gravityScale = 1;
    }
}
