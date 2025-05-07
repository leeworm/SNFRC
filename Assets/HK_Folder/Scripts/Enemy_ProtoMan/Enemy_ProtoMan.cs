using UnityEngine;

public class Enemy_ProtoMan : EnemyBase
{
    [Header("ProtoMan Specific")]
    public GameObject chargeShotPrefab;

    protected override void Start()
    {
        base.Start();

        // 플레이어 자동 할당 (태그가 "Player"인 오브젝트를 찾아서)
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Enemy_ProtoMan: Player not found! 'Player' 태그가 있는 오브젝트가 필요합니다.");
            return;
        }

        // 초기 상태 진입
        stateMachine.ChangeState(new ProtoManIdleState(this));
    }

    public void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
