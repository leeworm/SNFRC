using UnityEngine;

public class HK_PlayerHealth : HK_Health
{
    // 플레이어 고유의 죽음 처리
    protected override void Die()
    {
        base.Die();
        // 플레이어 죽음 시 추가 처리 (예: 게임 오버 화면)
    }

    // 플레이어 고유의 피해 처리
    public override void TakeDamage(int amount, Vector2 bulletPosition)
    {
        // 플레이어의 피해 처리 로직 (예: 체력 부족 시 UI 업데이트)
        base.TakeDamage(amount, bulletPosition);
    }
}
