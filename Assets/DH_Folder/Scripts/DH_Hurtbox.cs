using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DH_Hurtbox : MonoBehaviour
{
    public DH_Entity entity;

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (entity == null || entity.IsBlocking())
        {
            entity.ShowBlockEffect(transform.position);
            return;
        }

        entity.TakeDamage(damage, knockback);
        entity.ApplyKnockback(knockback);
        entity.ShowHitEffect(transform.position);
    }
}
