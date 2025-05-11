using UnityEngine;

public class HK_EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5f;
    public float lifeTime = 3f;
    public bool canPierce = false; // 피어싱 여부
    public Vector2 direction = Vector2.right; // 방향

    private void Start()
    {
        Destroy(gameObject, lifeTime);  // 생명 주기가 끝나면 총알 삭제
    }

    private void Update()
    {
        // 총알이 주어진 방향으로 이동
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌
        if (other.CompareTag("Player"))
        {
            HK_PlayerHealth player = other.GetComponent<HK_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage, transform.position);  // 플레이어에게 피해 입히고, 총알 위치 전달
            }

            if (!canPierce)  // 피어싱이 허용되지 않으면 총알 삭제
            {
                Destroy(gameObject);
            }
        }
        // 벽과 충돌 시
        else if (!other.CompareTag("Wall") && !other.isTrigger)
        {
            if (!canPierce)  // 피어싱이 허용되지 않으면 총알 삭제
            {
                Destroy(gameObject);
            }
        }
    }

    // 총알의 방향을 플레이어의 방향에 맞춰 설정하는 메서드
    public void SetDirection(bool isFacingRight)
    {
        direction = isFacingRight ? Vector2.right : Vector2.left;
    }
}


