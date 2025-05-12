using UnityEngine;

public class HK_BulletController : MonoBehaviour
{
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 2f;

    private Vector2 moveDirection = Vector2.right; // 기본값은 오른쪽

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HK_Health health = other.GetComponent<HK_Health>();
            if (health != null)
                health.TakeDamage(damage, transform.position);

            HK_EnemyHealth bossHealth = other.GetComponent<HK_EnemyHealth>();
            if (bossHealth != null)
                bossHealth.TakeDamage(damage, transform.position);

            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
