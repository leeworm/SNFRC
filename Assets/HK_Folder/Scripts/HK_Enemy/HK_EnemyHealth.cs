using UnityEngine;

public class HK_EnemyHealth : HK_Health
{
    [SerializeField] private float knockbackForce = 5f;

    public override void TakeDamage(int amount, Vector2 bulletPosition)
    {
        base.TakeDamage(amount, bulletPosition);
        ApplyKnockback(bulletPosition);
    }

    private void ApplyKnockback(Vector2 bulletPosition)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockDirection = ((Vector2)transform.position - bulletPosition).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
