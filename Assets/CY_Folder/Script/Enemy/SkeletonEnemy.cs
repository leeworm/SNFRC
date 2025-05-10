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

    protected override void RunAI()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // 벽 또는 낭떠러지 체크
        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);
        bool wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * moveDirection, checkDistance, groundLayer);

        if (noGround || wallHit)
            moveDirection *= -1;

        // 플레이어 기준 방향 결정
        float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);
        moveDirection = (int)directionToPlayer;

        FacePlayer();

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

            // 시각적 방향 보정
            Vector3 scale = arrow.transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (fireDirection.x > 0 ? 1 : -1);
            arrow.transform.localScale = scale;
        }

        yield return new WaitForSeconds(0.1f); // 약간의 여유시간 후 이동 재개
        isAttacking = false;
    }
}
