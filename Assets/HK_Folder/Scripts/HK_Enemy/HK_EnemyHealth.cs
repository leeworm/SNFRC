using UnityEngine;

public class HK_EnemyHealth : HK_Health
{
    // 부모 클래스의 IsDead를 숨기기 위해 new 키워드를 사용
    [SerializeField] private float knockbackForce = 5f;

    private void ApplyKnockback(Vector2 bulletPosition)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockDirection = ((Vector2)transform.position - bulletPosition).normalized;
            rb.linearVelocity = Vector2.zero; // 기존 속도 초기화
            rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
    public new bool IsDead
    {
        get { return base.IsDead; }
    }

    // 적 고유의 죽음 처리
    protected override void Die()
    {
        base.Die(); // 부모 클래스에서 기본적으로 죽음 처리
        // 적의 죽음 시 추가 처리 (예: 아이템 드랍)
        Debug.Log("Enemy has died!");

        // 예: 아이템 드랍
        // DropItem();

        // 애니메이션 설정 또는 상태 변경
        // animator.SetTrigger("Die");

        // 죽음 후 오브젝트 제거
        Destroy(gameObject, 2f); // 2초 후에 적 오브젝트 삭제
    }

    // 적 고유의 피해 처리
    public override void TakeDamage(int amount, Vector2 bulletPosition)
    {
        base.TakeDamage(amount, bulletPosition);

        Debug.Log("Enemy takes damage: " + amount);

        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null && !base.IsDead)
        {
            Debug.Log("Triggering Hit animation");
            animator.SetTrigger("Hit");
        }

        ApplyKnockback(bulletPosition); // ⬅ 넉백 추가

        if (base.IsDead)
        {
            Die();
        }
    }


}
