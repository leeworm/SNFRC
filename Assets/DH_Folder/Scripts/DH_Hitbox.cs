using System.Collections.Generic;
using UnityEngine;

public class DH_Hitbox : MonoBehaviour
{
    public int damage = 1;
    public Vector2 knockback = new Vector2(0, 0);
    public LayerMask targetLayer;
    public GameObject hitEffectPrefab;

    // 히트박스 영역 설정 (필요에 따라 Inspector에서 조정)
    public Vector2 boxSize = new Vector2(1f, 0.5f);
    public Vector2 boxOffset = Vector2.zero;
    public float boxAngle = 0f;

    // 이미 타격한 대상 저장
    private HashSet<Collider2D> hitTargets = new HashSet<Collider2D>();

    private void OnEnable()
    {
        hitTargets.Clear();
        CheckImmediateHit();
    }

    private void CheckImmediateHit()
    {
        Vector2 center = (Vector2)transform.position + boxOffset;
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, boxSize, boxAngle, targetLayer);

        foreach (var col in hits)
        {
            if (hitTargets.Contains(col))
                continue;

            DH_Hurtbox hurtbox = col.GetComponent<DH_Hurtbox>();
            if (hurtbox != null)
            {
                float direction = Mathf.Sign(col.transform.position.x - transform.position.x);
                Vector2 finalKnockback = new Vector2(knockback.x * direction, knockback.y);

                hurtbox.TakeDamage(damage, finalKnockback);

                if (hitEffectPrefab != null)
                    DH_EffectPoolManager.Instance.SpawnEffect("HitEffect", col.ClosestPoint(transform.position));

                hitTargets.Add(col);
            }
        }
    }

#if UNITY_EDITOR
    // 디버그용 박스 시각화
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 center = (Vector2)transform.position + boxOffset;
        Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, boxAngle), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}
