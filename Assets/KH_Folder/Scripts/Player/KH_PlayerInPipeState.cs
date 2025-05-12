using UnityEngine;

public class KH_PlayerInPipeState : KH_PlayerState
{
    private Vector3 inPos;
    private Vector3 outPos;

    public KH_PlayerInPipeState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        rb.linearVelocity = new Vector2(0, 0); // 힘 없애기
        player.PlayerIsTrigger(true); // 트리거로 만들기기
        rb.gravityScale = 0; // 중력 0 만들기기

        if(player.GetCollisionPipeNumber() == 0)
        {
            inPos = KH_GameManager.Instance.telepotPipe[0].vec;
            outPos = KH_GameManager.Instance.telepotPipe[1].vec + new Vector3(0,2.05f,0);
        }
        else if(player.GetCollisionPipeNumber() == 1)
        {
            inPos = KH_GameManager.Instance.telepotPipe[1].vec;
            outPos = KH_GameManager.Instance.telepotPipe[0].vec + new Vector3(0,2.05f,0);
        }

        player.isTelepot = false;
        player.isTelepotSuccess = false;
    }

    public override void Update()
    {
        base.Update();

        // 파이프 이동 끝나면 -> idle State
        if(player.isTelepotSuccess)
            stateMachine.ChangeState(player.idleState);

        if(!player.isTelepot)
            player.InTelepotPipe(inPos);
        else
            player.OutTelepotPipe(outPos);
    }

    public override void Exit()
    {
        base.Exit();
        
        player.PlayerIsTrigger(false);
        rb.gravityScale = 3;
    }
}
