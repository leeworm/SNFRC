using UnityEngine;

public class HK_BassRapidFire2State : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private float fireCooldown = 0.3f; // �߻� ��Ÿ�� (��)
    private float fireTimer = 0f; // ��Ÿ���� �����ϴ� Ÿ�̸�
    private int shotCount = 0; // �߻��� �Ѿ� ��
    private int maxShots = 4; // �ִ� �߻� Ƚ��

    public HK_BassRapidFire2State(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("RapidFire2"); // RapidFire2 �ִϸ��̼� ����
        shotCount = 0; // �߻�� �Ѿ� �� �ʱ�ȭ
        fireTimer = 0f; // Ÿ�̸� �ʱ�ȭ
    }

    public void Update()
    {
        fireTimer += Time.deltaTime; // �� �����Ӹ��� Ÿ�̸� ����

        // ��Ÿ���� ������, �ִ� �߻� Ƚ���� �������� ������ �߻�
        if (fireTimer >= fireCooldown && shotCount < maxShots)
        {
            bass.FireRapidShot2(); // RapidFire2 �߻�
            fireTimer = 0f; // Ÿ�̸� ����
            shotCount++; // �߻� Ƚ�� ����
        }

        // �ִ� �߻� Ƚ���� �����ϸ� ���� ��ȯ
        if (shotCount >= maxShots)
        {
            bass.stateMachine.ChangeState(new HK_BassMoveState(bass)); // �̵� ���·� ��ȯ
        }
    }

    public void Exit()
    {
        bass.animator.ResetTrigger("RapidFire2"); // RapidFire2 �ִϸ��̼� ����
    }

    public void AnimationFinishTrigger()
    {
        // �ִϸ��̼��� ������ �� �߰����� ó���� �ʿ��� ��� ���⿡ �ۼ�
    }
}
