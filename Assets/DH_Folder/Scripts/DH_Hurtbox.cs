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
            return;

        // lastKnockback 저장 (플레이어/에너미만)
        if (entity is DH_Player player)
            player.lastKnockback = knockback;
        else if (entity is DH_Enemy enemy)
            enemy.lastKnockback = knockback;

        entity.TakeDamage(damage, knockback);
        entity.ApplyKnockback(knockback);
    }
}
