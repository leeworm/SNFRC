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
        base.Die();
        // 적의 죽음 시 추가 처리 (예: 아이템 드랍)
    }

    // 적 고유의 피해 처리
    public override void TakeDamage(int amount, Vector2 bulletPosition)
    {
        // 적의 피해 처리 로직 (예: 넉백, 애니메이션 트리거)
        base.TakeDamage(amount, bulletPosition);
    }
}
