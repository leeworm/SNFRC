using System.Threading;
using UnityEngine;

public class KoopaSpinAttackState : KoopaState
{
    private int spinCounting;

    public KoopaSpinAttackState(Koopa _koopa, KoopaStateMachine _stateMachine, string _animBoolName) 
        : base(_koopa, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("스핀 공격 시작!");

        rb.gravityScale = 0;
        
        spinCounting = koopa.spinCount;
        koopa.isReadySpin = false;
        koopa.isScreenOutSpin = false;
        koopa.isEndSpin = false;
        koopa.spinReadyTimer = 10f; // 넉넉한 스핀 타이머
    }

    public override void Update()
    {
        base.Update();
        koopa.spinReadyTimer -= Time.deltaTime;

        if(spinCounting <= 0)
            koopa.EndSpin();

        if(koopa.isEndSpin)
            stateMachine.ChangeState(koopa.idleState);

        if(!koopa.isReadySpin)
            koopa.ReadySpin();

        if(!koopa.isScreenOutSpin)
            koopa.ScreenOutSpin();

        if(koopa.isScreenOutSpin && spinCounting > 0)
        {
            if(koopa.spinReadyTimer > 0f) // 1초 동안
                koopa.GoPlayerPosY();
            else if (koopa.spinReadyTimer <= -koopa.spinReadyTime) // 데미지 영역이 보이고, 공격하기 까지의 시간
                koopa.StartSpin(ref spinCounting);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        rb.gravityScale = 1;
    }
}
