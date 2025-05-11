using UnityEngine;

public class HK_EnemyHealth : HK_Health
{
    // 부모 클래스의 IsDead를 숨기기 위해 new 키워드를 사용
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
        base.TakeDamage(amount, bulletPosition); // 기본 체력 감소 처리

        // 적의 피해 처리 로직 (예: 넉백, 애니메이션 트리거)
        Debug.Log("Enemy takes damage: " + amount);

        // 애니메이션에서 'Hit' 트리거 추가
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Hit"); // 'Hit' 트리거 실행
        }

        // 체력이 0이 되면 Die() 호출
        if (base.IsDead)
        {
            Die();
        }
    }
}
