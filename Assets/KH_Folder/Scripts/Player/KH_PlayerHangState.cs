using UnityEngine;

public class KH_PlayerHangState : KH_PlayerState
{
    public KH_PlayerHangState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(0.0f, -0.1f);
        //rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public override void Update()
    {
        base.Update();

        if(player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.cutMoveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.SetParent(null);
        player.transform.rotation = Quaternion.identity;
    }
}
