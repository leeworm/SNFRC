using UnityEngine;

public class KH_PlayerGroundedState : KH_PlayerState
{
    public KH_PlayerGroundedState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if(!player.IsGroundDetected())
            stateMachine.ChangeState(player.fallState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            KH_SoundManager.Instance.PlaySFXSound("marioJump");
            stateMachine.ChangeState(player.jumpState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

}
