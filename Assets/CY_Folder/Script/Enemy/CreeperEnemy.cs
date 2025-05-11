using UnityEngine;
using System.Collections;

public class CreeperEnemy : B_Enemy
{
    public float explosionRange = 2.5f;
    public float explosionDelay = 0.8f;
    public int explosionDamage = 5;
    public LayerMask playerLayer;
    private bool isExploding = false;

    public GameObject explosionPrefab;
    public AudioClip explosionSound;

    protected override void RunAI()
    {
        if (isExploding || player == null || isDead) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // 벽 또는 낭떠러지 체크
        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        bool wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * moveDirection, checkDistance, groundLayer);

        if (noGround || wallHit)
        {
            moveDirection *= -1;

            //  스프라이트 방향 반전
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -moveDirection;
            transform.localScale = scale;
        }

        if (distance > 2f)
        {
            //  플레이어가 멀면 이동
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            //  가까우면 폭발 준비
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(Explode());
        }
    }


    private IEnumerator Explode()
    {
        isExploding = true;

        if (animator != null)
            animator.SetTrigger("Explode");
        if (explosionSound != null)
            B_AudioManager.Instance.PlaySFX(explosionSound);

        yield return new WaitForSeconds(explosionDelay);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (player != null && Vector2.Distance(transform.position, player.position) <= explosionRange)
        {
            B_PlayerHealth playerHealth = player.GetComponent<B_PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(explosionDamage, transform.position);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * moveDirection * checkDistance);
        }
    }
}
