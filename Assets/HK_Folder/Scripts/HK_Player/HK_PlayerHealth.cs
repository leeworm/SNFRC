using UnityEngine;

public class HK_PlayerHealth : MonoBehaviour
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
            // ��Ʈ �ִϸ��̼�, ���� �ð� �� ó��
        }
    }

    private void Die()
    {
        // ���ӿ��� ó��
        Debug.Log("Player Died");
    }
}
