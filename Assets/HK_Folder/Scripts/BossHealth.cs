using UnityEngine;

public class BossHealth : MonoBehaviour
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
        // 죽는 연출, 비활성화, 애니메이션 트리거 등
        GetComponent<Animator>().SetTrigger("Die");
        // 필요시 콜라이더/AI 비활성화
    }
}
