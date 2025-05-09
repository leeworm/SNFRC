using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;

    public UnityEvent OnDeath; // 인스펙터에서 연결 가능
    public Slider healthBar;   // 체력바 UI 연결용

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
            OnDeath.Invoke(); // 상태머신 전환 or 삭제 등 이벤트로 연결
    }

    internal float GetHealthPercent()
    {
        throw new NotImplementedException();
    }
}
