using UnityEngine;

public class PigminEnemy : B_Enemy
{
    protected override void RunAI()
    {
        if (isDead) return; // 죽었으면 AI 실행 안 함

        if (Vector2.Distance(transform.position, player.position) < 6f)
        {
            // 플레이어 추적
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            FacePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            B_PlayerHealth player = other.GetComponent<B_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(3, transform.position); // ← 좀비 공격력 1, 자신의 위치를 전달
            }
        }
    }

    protected override void Die()
    {
        base.Die(); // Enemy.cs에 있는 DeathSequence 실행
    }
}

