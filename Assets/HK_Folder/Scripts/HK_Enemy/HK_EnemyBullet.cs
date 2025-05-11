using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HK_EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5f;
    public float lifeTime = 3f;
    public bool canPierce = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 일정 시간 후 파괴
        Destroy(gameObject, lifeTime);
    }

    // 외부에서 방향 설정 (💡 방향과 속도 함께 적용)
    public void SetDirection(Vector2 newDirection)
    {
        rb.linearVelocity = newDirection.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HK_PlayerHealth player = other.GetComponent<HK_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage, transform.position);
            }

            if (!canPierce)
                Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") && !other.isTrigger)
        {
            if (!canPierce)
                Destroy(gameObject);
        }
    }
}
