using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 10;                   // 공격력
    public Collider2D attackHitbox;          // 공격 판정용 콜라이더 (Trigger)

    private void Awake()
    {
        // 시작 시엔 비활성화 (안 보이게)
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    /// <summary>
    /// 공격 시작 시 호출 (예: 애니메이션 이벤트에서 호출)
    /// </summary>
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    /// <summary>
    /// 공격 끝났을 때 호출
    /// </summary>
    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hitbox가 Trigger로 충돌했을 때 실행됨
        if (other.CompareTag("Enemy"))
        {
            Health enemy = other.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Enemy {other.name} hit for {damage} damage!");
            }
        }
    }
}
