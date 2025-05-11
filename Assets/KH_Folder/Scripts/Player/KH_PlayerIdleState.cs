using UnityEngine;

public class KH_PlayerIdleState : KH_PlayerGroundedState
{
    public KH_PlayerIdleState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocity = new Vector2(0, 0);
    }
    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected()) // 벽 닿을때 고정
            return;

        if(player.IsPipeDetected()) // 파이프 닿을때
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // 파이프가 2개라면, 파이프 안에 들어감 inPipeState
                if(KH_GameManager.Instance.telepotPipe[0].havePipe && KH_GameManager.Instance.telepotPipe[1].havePipe)
                    stateMachine.ChangeState(player.inPipeState);
            }
        }

        if (xInput != 0)
            stateMachine.ChangeState(player.moveState);

    }
    public override void Exit()
    {
        base.Exit();
    }
}
