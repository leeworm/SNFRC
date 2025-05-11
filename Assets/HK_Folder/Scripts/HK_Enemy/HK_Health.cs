using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HK_Health : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public float invincibleDuration = 1f;
    public float flashInterval = 0.1f;
    public Slider healthBar;
    public UnityEvent OnDeath;

    protected bool isDead = false;  // isDead는 기본적으로 부모 클래스에서 관리됩니다.

    // IsDead 프로퍼티 추가
    public bool IsDead => isDead;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void TakeDamage(int amount, Vector2 bulletPosition)
    {
        if (isInvincible || isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;
        animator?.SetTrigger("Die");
        OnDeath?.Invoke();
        StartCoroutine(WaitForDieAnimation());
    }

    private IEnumerator WaitForDieAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName("Die") || stateInfo.normalizedTime < 1f)
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
        Destroy(gameObject);
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float timer = 0f;
        while (timer < invincibleDuration)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }
        if (spriteRenderer != null) spriteRenderer.enabled = true;
        isInvincible = false;
    }

    public void Heal(int amount)
    {
        if (isDead) return;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public float GetHealthPercent() => (float)currentHealth / maxHealth;

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }
}
