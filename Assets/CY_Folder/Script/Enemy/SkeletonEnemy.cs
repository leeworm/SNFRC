using UnityEngine;
using System.Collections;

public class SkeletonEnemy : B_Enemy
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float attackRange = 5f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private bool isAttacking = false;

    public AudioClip shootSound;
    public AudioSource audioSource;
    

    protected override void RunAI()
    {
        if (isDead || player == null) return;

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

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown && !isAttacking)
        {
            lastAttackTime = Time.time;
            StartCoroutine(ShootArrowWithDelay());
        }
        else if (!isAttacking)
        {
            // 걷기
            rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

            // 걷기 애니메이션
            if (animator != null)
                animator.Play("Skeleton_Move");
        }

    }

    private IEnumerator ShootArrowWithDelay()
    {
        isAttacking = true;

        // 정지 + 애니메이션
        rb.linearVelocity = Vector2.zero;
        if (animator != null)
            animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f); // 공격 모션 기다리기

        if (arrowPrefab != null && shootPoint != null)
        {
            Vector2 fireDirection = (player.position.x < transform.position.x) ? Vector2.left : Vector2.right;

            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        
            if (arrowRb != null)
            {
                arrowRb.linearVelocity = fireDirection * 10f;
            }

            // 🔊 화살 소리 추가
            if (shootSound != null && audioSource != null)
            audioSource.PlayOneShot(shootSound);
            
            // 시각적 방향 보정
            Vector3 scale = arrow.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (fireDirection.x > 0 ? 1 : -1);
            arrow.transform.localScale = scale;
        }

        yield return new WaitForSeconds(0.1f); // 약간의 여유시간 후 이동 재개
        isAttacking = false;
    }
}
