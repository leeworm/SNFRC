using System;
using System.Collections;
using UnityEngine;

public class KH_PlayerShotState : KH_PlayerState
{
    private float shotDelay = 0.2f; // 총알 발사 딜레이

    public KH_PlayerShotState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.linearVelocityX = 0; // x축 속도 초기화
    }

    public override void Update()
    {
        base.Update();

        shotDelay -= Time.deltaTime;
        if(shotDelay < 0)
        {
            ShootBullet();
            shotDelay = 0.2f;
        }
        
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if(!player.IsGroundDetected())
                stateMachine.ChangeState(player.fallState);
            else if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

    private void ShootBullet()
    {
        //bulletPool.GetBullet(player.FireballSpawnPoint);
        KH_BulletPool.Instance.GetBullet(player.FireballSpawnPoint, player.facingDir);
    }

}
