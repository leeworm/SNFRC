using UnityEngine;

public class HK_BulletController : MonoBehaviour
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
            HK_Health health = other.GetComponent<HK_Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // ���� ü�� ó��
            HK_BossHealth bossHealth = other.GetComponent<HK_BossHealth>();
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

