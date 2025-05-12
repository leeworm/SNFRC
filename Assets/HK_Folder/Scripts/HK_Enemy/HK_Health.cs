using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class HK_Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 300;
    public float invincibleDuration = 1f;
    public float flashInterval = 0.1f;
    public Slider healthBar;

    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip deathSound;
    [Range(0f, 1f)] public float hitVolume = 0.7f;
    [Range(0f, 1f)] public float deathVolume = 0.9f;

    public UnityEvent OnDeath;

    protected int currentHealth;
    protected bool isDead = false;
    public bool IsDead => isDead;

    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int amount, Vector2 bulletPosition)
    {
        if (isInvincible || isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        PlaySound(hitSound, hitVolume);

        if (!isDead)
        {
            animator?.SetTrigger("Hit");
            StartCoroutine(InvincibilityCoroutine());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;
        isDead = true;

        animator?.SetTrigger("Die");
        PlaySound(deathSound, deathVolume);
        OnDeath?.Invoke();

        StartCoroutine(WaitForDieAnimation());
    }

    private IEnumerator WaitForDieAnimation()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            while (!stateInfo.IsName("Die") || stateInfo.normalizedTime < 1f)
            {
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }
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

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvincible = false;
    }

    protected void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
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
