using UnityEngine;
using System.Collections;

public class BlazeEnemy : B_Enemy
{
    public GameObject fireballPrefab;
    public Transform shootPoint;
    public float attackRange = 5f;
    public float fireballSpeed = 8f;
    public float fireInterval = 0.3f;
    public float attackCooldown = 5f;

    private bool isAttacking = false;
    private float lastAttackTime;

    public AudioClip fireballSound;

    protected override void RunAI()
    {
        if (isDead || player == null || isAttacking) return;

       if (Vector2.Distance(transform.position, player.position) < 6f)
        {
            // 플레이어 추적
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        FacePlayer();
        }
        // 플레이어를 바라보게 함
                else
        {
            Patrol();
        }
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(ShootFireballs());
        }
        else
        {
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
            
        }
    }

    private IEnumerator ShootFireballs()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        for (int i = 0; i < 3; i++)
        {
            if (fireballPrefab != null && shootPoint != null)
            {
                Vector2 fireDir = (player.position.x < transform.position.x) ? Vector2.left : Vector2.right;

                GameObject fireball = Instantiate(fireballPrefab, shootPoint.position, Quaternion.identity);
                Rigidbody2D fireRb = fireball.GetComponent<Rigidbody2D>();

                if (fireRb != null)
                    fireRb.linearVelocity = fireDir * fireballSpeed;

                Vector3 scale = fireball.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * (fireDir.x > 0 ? 1 : -1);
                fireball.transform.localScale = scale;
                AudioSource.PlayClipAtPoint(fireballSound, transform.position);
            }

            yield return new WaitForSeconds(fireInterval);
        }

        lastAttackTime = Time.time;
        isAttacking = false;
    }
}
