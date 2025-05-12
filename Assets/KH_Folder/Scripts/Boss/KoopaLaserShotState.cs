using UnityEngine;

public class KoopaLaserShotState : KoopaState
{
    private bool isCreate;

    private bool isCharge;

    public KoopaLaserShotState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isCreate = false;
        isCharge = false;

        stateTimer = koopa.fireLaserTime; // 상태 지속 시간
        
        koopa.rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <= 0)
        {
            koopa.stateMachine.ChangeState(koopa.idleState);
            return;
        }

        GoPattern(new Vector3(0, -190, 0));
    }

    public override void Exit()
    {
        base.Exit();

        koopa.DestoryLaser();
        
        koopa.rb.gravityScale = 1;
    }

    public void GoPattern(Vector3 _midPos)
    {
        if(koopa.transform.position == _midPos)
        {
            if(stateTimer <= koopa.fireLaserTime - 5)
            {
                if(!isCreate)
                {
                    koopa.CreateLaser();
                    isCreate = true;
                }
                else
                    koopa.StartLaser();
            }
            else
            {
                if(!isCharge)
                {
                    KH_EffectManager.Instance.PlayEffect("ChargeEffect", koopa.transform.position);
                    isCharge = true;
                }
            }
        }
        else
        {
            koopa.transform.position = Vector3.MoveTowards(koopa.transform.position, _midPos, koopa.moveSpeed * Time.deltaTime);
        }
    }
}
