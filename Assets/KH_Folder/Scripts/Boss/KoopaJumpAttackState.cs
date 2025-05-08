using UnityEngine;

public class KoopaJumpAttackState : KoopaState
{
    private bool isAttack;

    private bool isEffect;

    public KoopaJumpAttackState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isAttack = false; // 공격 초기화
        isEffect = false; // 이펙트 초기화
        stateTimer = 5f;

        koopa.JumpUp();
    }

    public override void Update()
    {
        base.Update();
        if(koopa.IsGroundDetected())
        {
            Debug.Log("점프 공격 종료");
            CameraShake.Instance.Shake(); // 카메라 흔들림 효과

            if(!isEffect && stateTimer <= 2.5f) // 이펙트 생성
            {
                KH_EffectManager.Instance.PlayEffect("FogEffect", koopa.transform.position + new Vector3(0, 1, 0)); // 이펙트 생성
                isEffect = true;
            }

            if(stateTimer <= 2f) // 바닥에 닿으면 점프 속도 초기화
            {
                koopa.stateMachine.ChangeState(koopa.idleState);
            }
        }
        

        if(stateTimer <= 3f && !isAttack)
        {
            koopa.JumpDown();
            isAttack = true;
        }
        
        if(koopa.transform.position.y > 15f)
        {
            koopa.GoPlayerPos();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
