using UnityEngine;

public class HK_BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        if (IsDead)
        {
            Die();
        }
    }

    void Die()
    {
        // �״� ����, ��Ȱ��ȭ, �ִϸ��̼� Ʈ���� ��
        GetComponent<Animator>().SetTrigger("Die");
        // �ʿ�� �ݶ��̴�/AI ��Ȱ��ȭ
    }
}
