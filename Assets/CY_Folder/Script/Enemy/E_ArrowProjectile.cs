using UnityEngine;

public class E_ArrowProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    public int damage = 1;
    public float lifetime = 2f;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Player"))
        {
            hasHit = true;
            B_PlayerHealth player = other.GetComponent<B_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage, transform.position); // 원하는 데미지 값
            }
            Destroy(gameObject); // 바로 사라짐
        }
        else if (other.CompareTag("Ground"))
        {
            hasHit = true;
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.SetParent(other.transform); // 벽에 고정된 것처럼 보이게
            Destroy(gameObject, 2f); // 2초 후 제거
        }
    }
}
