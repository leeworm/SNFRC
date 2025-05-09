
using UnityEngine;

public class BassDeathState : IEnemyState
{
    private Enemy_Bass bass;
    private float deathTimer = 2f;

    public BassDeathState(Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetTrigger("Die");
        bass.SetVelocity(Vector2.zero); // ������ ����

        // ��Ÿ �ʿ� ���: �ݶ��̴� ��Ȱ��ȭ ��
        if (bass.GetComponent<Collider2D>()) bass.GetComponent<Collider2D>().enabled = false;
    }

    public void Update()
    {
        deathTimer -= Time.deltaTime;
        if (deathTimer <= 0)
        {
            GameObject.Destroy(bass.gameObject); // ���� �ð� �� ����
        }
    }

    public void Exit() { }

    public void AnimationFinishTrigger()
    {
        throw new System.NotImplementedException();
    }
}
