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
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            if (!canPierce)
            {
                Destroy(gameObject);
            }
        }
        else if (!other.CompareTag("Enemy") && !other.isTrigger) // 벽이나 다른 오브젝트와 충돌 시
        {
            if (!canPierce)
            {
                Destroy(gameObject);
            }
        }
    }
}
