using UnityEngine;

public class ProtoManAttackState : IEnemyState
{
    private Enemy_ProtoMan protoMan;
    private bool hasShot = false;
    private Transform player;

    public ProtoManAttackState(Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Enter()
    {
        hasShot = false; // 공격할 준비 상태 초기화
        protoMan.animator.Play("ProtoMan_Attack1", 0); // 애니메이션 시작
    }

    public void Update()
    {
        if (!hasShot)
        {
            Shoot(); // 첫 번째 공격이 발사되도록 호출
            hasShot = true; // 발사 완료 처리
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        // 애니메이션이 끝나면 대기 상태로 전환
        protoMan.stateMachine.ChangeState(new ProtoManIdleState(protoMan));
    }

    private void Shoot()
    {
        if (protoMan.attackPrefab != null && protoMan.firePoint != null && player != null)
        {
            // 불렛 생성
            GameObject bullet = Object.Instantiate(
                protoMan.attackPrefab,
                protoMan.firePoint.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 플레이어 방향 계산
                Vector2 direction = (player.position - protoMan.firePoint.position).normalized;

                // 플레이어가 왼쪽 또는 오른쪽에 있을 때 프로토맨의 방향을 확인하고 반영
                if (protoMan.transform.localScale.x < 0)
                {
                    // 프로토맨이 왼쪽을 보고 있을 때, 반대 방향으로 발사
                    direction = -direction;
                }

                // 불렛에 방향 적용
                rb.linearVelocity = direction * 10f; // 원하는 속도만큼 설정
            }
        }
    }

}

