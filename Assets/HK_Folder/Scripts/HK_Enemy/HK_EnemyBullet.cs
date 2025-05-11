using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HK_EnemyBullet : MonoBehaviour
{
    public enum BulletType
    {
        Type1,  // 첫 번째 불렛 타입
        Type2   // 두 번째 불렛 타입
    }

    public BulletType bulletType;  // 불렛 타입을 지정
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
        Destroy(gameObject, lifeTime);  // 일정 시간 후 파괴
    }

    // 외부에서 방향 설정 (💡 방향과 속도 함께 적용)
    public void SetDirection(Vector2 newDirection)
    {
        rb.linearVelocity = newDirection.normalized * speed;  // linearVelocity에서 velocity로 변경
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision(other);
        }
        else if (other.CompareTag("Wall") && !other.isTrigger)
        {
            HandleWallCollision();
        }
    }

    private void HandlePlayerCollision(Collider2D other)
    {
        HK_PlayerHealth player = other.GetComponent<HK_PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage, transform.position);
        }

        HandleBulletDestruction();
    }

    private void HandleWallCollision()
    {
        HandleBulletDestruction();
    }

    private void HandleBulletDestruction()
    {
        if (!canPierce)
        {
            Destroy(gameObject);  // 관통할 수 없으면 불렛 파괴
        }
    }
}
