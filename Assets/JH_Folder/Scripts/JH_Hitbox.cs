using System.Collections.Generic;
using UnityEngine;

public class JH_Hitbox : MonoBehaviour
{
    public float damage = 10f;
    public string targetTag = "Enemy";

    private HashSet<Collider2D> hitTargetsThisActivation; //�ߺ�Ÿ�ݹ���
    void OnEnable()
    {
        if (hitTargetsThisActivation == null)
        {
            hitTargetsThisActivation = new HashSet<Collider2D>();
        }
        hitTargetsThisActivation.Clear(); // Ȱ��ȭ�� ������ ���� ��� ��� �ʱ�ȭ
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �̹� �̹� ���� Ȱ��ȭ �߿� ���� ����̸� ����
        if (hitTargetsThisActivation.Contains(other))
        {
            return;
        }

        // ������ Ÿ�� �±�/���̾�� ��ġ�ϴ��� Ȯ��
        if (other.CompareTag(targetTag)) // �Ǵ� (targetLayer.value & (1 << other.gameObject.layer)) > 0
        {
            Debug.Log(gameObject.name + "�� " + other.name + "���� ����!");

            // ����(other)���� �������� �ִ� ����
            // ��: other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            // �Ǵ� IDamageable �������̽� ���
            IDamageable damageableTarget = other.GetComponent<IDamageable>();
            if (damageableTarget != null)
            {
                damageableTarget.TakeDamage(damage);
                hitTargetsThisActivation.Add(other); // ���� ��� ��Ͽ� �߰�

                // ���⿡ Ÿ�� ����Ʈ ����, ���� ��� ���� �ڵ� �߰�
            }
        }
    }
}

public interface IDamageable
{
    void TakeDamage(float amount);
}
