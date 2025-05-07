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
        // �� �Ǵ� ������ �ε����� ���
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            var enemyHealth = other.GetComponent<BossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        // �� ���� �ٸ� ������Ʈ�� ������ �׳� �ı�
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
