using UnityEngine;

public class KH_PlayerFallState : KH_PlayerState
{
    public KH_PlayerFallState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    
        if(player.isStage1)
        {
            player.jumpAttackCollider.SetActive(true); // 점프 공격 콜라이더 활성화
            player.canHit = false; // 무적
        }
    }

    public override void Update()
    {
        base.Update();

        // 점프 중에 땅에 닿으면 점프 취소
        if (player.IsGroundDetected() && stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();
        
        if(player.isStage1)
        {
            player.jumpAttackCollider.SetActive(false); // 점프 공격 콜라이더 비활성화
            player.canHit = true; // 무적 해제
        }
    }


    
}
