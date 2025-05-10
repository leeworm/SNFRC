using System;
using UnityEngine;

public class DH_PlayerBackstepState : DH_PlayerGroundedState
{
    private float backstepDuration = 0.25f; // 백스텝 유지 시간
    private float landingCheckDelay = 0.25f;  // 착지 감지 유예 시간
    private float timer;
    private int direction;
    private float delayTimer;
    private float backstepSpeed;

    private bool canLand = false;

    public DH_PlayerBackstepState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName, int _direction)
        : base(_player, _stateMachine, _animBoolName)
    {
        this.direction = _direction;
    }
    public override void Enter()
    {
        base.Enter();
        player.isBusy = true;
        timer = backstepDuration;
        delayTimer = landingCheckDelay;
        canLand = false;
        backstepSpeed = player.dashSpeed * 1.5f;
        float jumpforce = 13.5f;
        //player.anim.SetBool("Backstep", true);
        // 순간적으로 속도 부여
        player.SetVelocity(direction * backstepSpeed, jumpforce);
    }

    public override void Update()
    {
        base.Update();

        // 일정 시간 지나기 전엔 착지 무시
        if (!canLand)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0f)
                canLand = true;
        }

        float backstepSpeed = player.dashSpeed;
        player.SetVelocity(direction * backstepSpeed, rb.linearVelocity.y);

        if (canLand && player.IsGroundDetected())
        {
            player.SetVelocity(0, rb.linearVelocity.y);
            stateMachine.ChangeState(player.idleState);
            return;
        }
                
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
            stateMachine.ChangeState(player.idleState);
        }

        if (Input.GetKeyDown(KeyCode.Z))  
            return;
    }

    public override void Exit()
    {
        base.Exit();
        player.isBusy = false;
        //player.anim.SetBool("Backstep", false);
        player.SetVelocity(0, rb.linearVelocity.y);
    }
}
