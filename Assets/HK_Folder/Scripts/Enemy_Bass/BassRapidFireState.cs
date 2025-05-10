using UnityEngine;

public class BassRapidFireState : IEnemyState
{
    private Enemy_Bass bass;
    private float fireRate = 0.2f; // ���� �߻� �ð� ����
    private float fireTimer; // �߻� Ÿ�̸�
    private int shotsFired; // �߻�� �Ѿ��� ��
    private int maxShots = 5; // �ִ� �߻� Ƚ��

    public BassRapidFireState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        fireTimer = 0f;
        shotsFired = 0;
        bass.animator.SetTrigger("RapidFire"); // RapidFire �ִϸ��̼� Ʈ����
    }

    public void Update()
    {
        // �߻� Ÿ�̸Ӱ� fireRate�� ������ �߻�
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate && shotsFired < maxShots)
        {
            fireTimer = 0f;
            shotsFired++;

            // �߻�� �Ѿ� ����
            GameObject shot = GameObject.Instantiate(bass.rapidShotPrefab, bass.firePoint.position, Quaternion.identity);
            Vector2 direction = (bass.player.position - bass.firePoint.position).normalized;
            shot.GetComponent<Rigidbody2D>().linearVelocity = direction * 12f;  // �Ѿ��� �ӵ� ����
        }

        // �߻� Ƚ���� �ִ�ġ�� �����ϸ� �ٸ� ���·� ��ȯ
        if (shotsFired >= maxShots)
        {
            bass.stateMachine.ChangeState(new BassJumpState(bass)); // �߻� �� ���� ���·� ����
        }
    }

    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼��� ������ �� ȣ��Ǵ� �޼���
        if (shotsFired < maxShots)
        {
            // �߻� �߿� �ִϸ��̼��� ������ ��� �߻��ϵ��� Ʈ����
            bass.animator.SetTrigger("RapidFire");
        }
        else
        {
            // ��� �߻簡 �����ٸ� ���� ���·� �Ѿ���� ó��
            bass.stateMachine.ChangeState(new BassIdleState(bass));
        }
    }

    public void Exit()
    {
        // ���� ���� �� ó���� ����
    }
}
