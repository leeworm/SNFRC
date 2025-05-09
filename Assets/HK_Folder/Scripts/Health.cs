using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    public UnityEvent OnDeath; // �ν����Ϳ��� ���� ����
    public Slider healthBar;   // ü�¹� UI �����

    private void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log($"{gameObject.name} took damage: {amount}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");

        if (OnDeath != null)
            OnDeath.Invoke(); // ���¸ӽ� ��ȯ or ���� �� �̺�Ʈ�� ����
    }

    internal float GetHealthPercent()
    {
        throw new NotImplementedException();
    }
}
