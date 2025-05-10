using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적 또는 보스에게 명중
        if (other.CompareTag("Enemy"))
        {
            // 일반 적의 체력 처리
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // 보스 체력 처리
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        // 벽 또는 다른 장애물과 충돌했을 때
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

