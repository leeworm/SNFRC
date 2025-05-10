using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class B_PlayerHealth : MonoBehaviour
{
    public int maxHearts = 10;
    private int currentHearts;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Coroutine contactDamageCoroutine;

    private bool isInvincible = false;
    private float invincibleDuration = 1f;

    private HashSet<B_Enemy> recentlyHitEnemies = new HashSet<B_Enemy>();
    private float contactCooldown = 1f; // 중복 데미지 방지 시간

    private void Start()
    {
        currentHearts = maxHearts;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 attackerPos)
    {
            if (isInvincible || currentHearts <= 0) return;

        currentHearts -= damage;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);

        Debug.Log($"❤️ 남은 하트: {currentHearts}");

        KnockbackFrom(attackerPos);
        StartCoroutine(HitFlash());
        StartCoroutine(InvincibilityCooldown()); // ✅ 무적 코루틴 시작

        if (currentHearts <= 0)
        {
            Die();
        }
    }
    private IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("💀 플레이어 사망");
    }

    private void KnockbackFrom(Vector2 attackerPos)
    {
        if (rb == null) return;

        float dir = Mathf.Sign(transform.position.x - attackerPos.x);
        Vector2 force = new Vector2(dir * -6f, 2f);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator HitFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator DealContactDamage(B_Enemy enemy)
    {
        

        while (true)
        {
            if (!recentlyHitEnemies.Contains(enemy))
            {
                TakeDamage(enemy.contactDamage, enemy.transform.position);
                recentlyHitEnemies.Add(enemy);
                StartCoroutine(RemoveFromRecentlyHit(enemy, contactCooldown));
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RemoveFromRecentlyHit(B_Enemy enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        recentlyHitEnemies.Remove(enemy);
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
         B_Enemy enemy = other.GetComponent<B_Enemy>();

        // 히트박스 무시
        if (enemy != null && !other.CompareTag("Hitbox"))
        {
            if (contactDamageCoroutine == null)
                contactDamageCoroutine = StartCoroutine(DealContactDamage(enemy));
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
       B_Enemy enemy = other.GetComponent<B_Enemy>();
        if (enemy != null && contactDamageCoroutine != null)
        {
            StopCoroutine(contactDamageCoroutine);
            contactDamageCoroutine = null;
        }
    }

    public int GetCurrentHearts()
    {
        return currentHearts;
    }
}
