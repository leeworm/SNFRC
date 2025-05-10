using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private bool hasHit = false;

    public WeaponType type;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction * speed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        if (!hasHit && rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        // ✅ 적 or 벽에만 반응
        if (!other.CompareTag("Enemy") && !other.CompareTag("Ground"))
            return;

        hasHit = true;

        rb.linearVelocity = Vector2.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        // ✅ 적에게 "붙이기"
        transform.SetParent(other.transform); // 움직이는 적에 고정됨

        // ✅ 필요하면 데미지 처리
        if (other.CompareTag("Enemy"))
        {
            B_Enemy enemy = other.GetComponent<B_Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10); // 너의 데미지 시스템에 맞춰 수정 가능
                enemy.KnockbackFrom(transform.position);
            }
        }

        Destroy(gameObject, 3f); // 3초 후 제거
    }
}
