using UnityEngine;

public class Enemy_ProtoMan : EnemyBase
{
    [Header("ProtoMan Specific")]
    public GameObject chargeShotPrefab;

    protected override void Start()
    {
        base.Start();

        // �÷��̾� �ڵ� �Ҵ� (�±װ� "Player"�� ������Ʈ�� ã�Ƽ�)
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Enemy_ProtoMan: Player not found! 'Player' �±װ� �ִ� ������Ʈ�� �ʿ��մϴ�.");
            return;
        }

        // �ʱ� ���� ����
        stateMachine.ChangeState(new ProtoManIdleState(this));
    }

    public void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
