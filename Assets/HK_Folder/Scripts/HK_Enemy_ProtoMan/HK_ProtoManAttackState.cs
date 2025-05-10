using UnityEngine;

public class HK_ProtoManAttackState : HK_IEnemyState
{
    private HK_Enemy_ProtoMan protoMan;
    private bool hasShot = false;
    private Transform player;

    public HK_ProtoManAttackState(HK_Enemy_ProtoMan protoMan)
    {
        this.protoMan = protoMan;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Enter()
    {
        hasShot = false; // ������ �غ� ���� �ʱ�ȭ
        protoMan.animator.Play("ProtoMan_Attack1", 0); // �ִϸ��̼� ����
    }

    public void Update()
    {
        if (!hasShot)
        {
            Shoot(); // ù ��° ������ �߻�ǵ��� ȣ��
            hasShot = true; // �߻� �Ϸ� ó��
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼��� ������ ��� ���·� ��ȯ
        protoMan.stateMachine.ChangeState(new HK_ProtoManIdleState(protoMan));
    }

    private void Shoot()
    {
        if (protoMan.attackPrefab != null && protoMan.firePoint != null && player != null)
        {
            // �ҷ� ����
            GameObject bullet = Object.Instantiate(
                protoMan.attackPrefab,
                protoMan.firePoint.position,
                Quaternion.identity
            );

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �÷��̾� ���� ���
                Vector2 direction = (player.position - protoMan.firePoint.position).normalized;

                // �÷��̾ ���� �Ǵ� �����ʿ� ���� �� ��������� ������ Ȯ���ϰ� �ݿ�
                if (protoMan.transform.localScale.x < 0)
                {
                    // ��������� ������ ���� ���� ��, �ݴ� �������� �߻�
                    direction = -direction;
                }

                // �ҷ��� ���� ����
                rb.linearVelocity = direction * 10f; // ���ϴ� �ӵ���ŭ ����
            }
        }
    }

}

