using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5f;
    public float lifeTime = 3f;
    public bool canPierce = false; // 관통 여부
    public Vector2 direction = Vector2.right; // 방향

    private void Start()
    {
        Destroy(gameObject, lifeTime);  // 일정 시간이 지나면 총알을 자동으로 삭제
    }

    private void Update()
    {
        // 지정된 방향으로 총알을 이동
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 플레이어와 충돌 시
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);  // 플레이어에게 데미지 적용
            }

            if (!canPierce)  // 관통하지 않으면 총알 삭제
            {
                Destroy(gameObject);
            }
        }
        else if (!other.CompareTag("Enemy") && !other.isTrigger)  // 적과 충돌하지 않으며, 트리거가 아닌 다른 객체와 충돌 시
        {
            if (!canPierce)  // 관통하지 않으면 총알 삭제
            {
                Destroy(gameObject);
            }
        }
    }
}
