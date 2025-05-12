using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DH_Hurtbox : MonoBehaviour
{
    public DH_Entity entity;

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (entity == null)
            return;

        if (entity.IsBlocking())
        {
            return;
        }

        // 블록 상태가 아닐 때만 데미지 처리 및 히트 이펙트 표시
        entity.TakeDamage(damage, knockback);
        entity.ApplyKnockback(knockback);
    }
}
