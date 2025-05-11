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
        // 적과 충돌했을 때
        if (other.CompareTag("Enemy"))
        {
            // 적이 받은 피해 처리
            HK_Health health = other.GetComponent<HK_Health>();
            if (health != null)
            {
                // 총알 위치를 전달하여 피해 처리
                health.TakeDamage(damage, transform.position); // transform.position이 총알의 위치
            }

            // 보스와 충돌했을 때 (보스도 HK_Health를 상속받았으므로 처리 가능)
            HK_EnemyHealth bossHealth = other.GetComponent<HK_EnemyHealth>();
            if (bossHealth != null)
            {
                // 총알 위치를 전달하여 피해 처리
                bossHealth.TakeDamage(damage, transform.position); // transform.position이 총알의 위치
            }

            Destroy(gameObject); // 총알을 삭제
        }

        // 벽과 충돌했을 때
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // 벽과 충돌하면 총알 삭제
        }
    }
}
