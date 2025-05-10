using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어의 몸통과만 충돌 감지
        if (other.CompareTag("Player"))
        {
            B_PlayerHealth ph = other.GetComponent<B_PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(1, transform.position);
            }
        }
    }
}
