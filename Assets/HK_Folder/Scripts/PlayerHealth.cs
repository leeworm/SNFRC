using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // 히트 애니메이션, 무적 시간 등 처리
        }
    }

    private void Die()
    {
        // 게임오버 처리
        Debug.Log("Player Died");
    }
}
