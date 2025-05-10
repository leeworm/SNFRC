using System;
using System.Collections;
using UnityEngine;

public class KH_PlayerShotState : KH_PlayerState
{
    private float shotDelay = 0.2f; // 총알 발사 딜레이
    private float tempVelocityY; // y축 속도 임시 저장

    public KH_PlayerShotState(KH_Player _player, KH_PlayerStateMachine _stateMachine, string _animBoolName) 
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        tempVelocityY = rb.linearVelocityY; // 현재 y축 속도 저장

        rb.linearVelocityX = 0; // x축 속도 초기화
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; // y축 고정 및 회전 고정
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
        rb.linearVelocityY = tempVelocityY; // y축 속도 복원
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    private void ShootBullet()
    {
        //bulletPool.GetBullet(player.FireballSpawnPoint);
        KH_BulletPool.Instance.GetBullet(player.FireballSpawnPoint, player.facingDir);
    }

}
