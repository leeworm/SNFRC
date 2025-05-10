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
        // �� �Ǵ� �������� ����
        if (other.CompareTag("Enemy"))
        {
            // �Ϲ� ���� ü�� ó��
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // ���� ü�� ó��
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        // �� �Ǵ� �ٸ� ��ֹ��� �浹���� ��
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

