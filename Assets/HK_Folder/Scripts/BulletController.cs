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
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적 또는 보스에 부딪혔을 경우
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            var enemyHealth = other.GetComponent<BossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        // 벽 같은 다른 오브젝트에 닿으면 그냥 파괴
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
