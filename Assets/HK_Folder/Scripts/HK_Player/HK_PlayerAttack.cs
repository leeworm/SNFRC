using UnityEngine;

public class HK_PlayerAttack : MonoBehaviour
{
    public int damage = 10;                    // 공격력
    public Collider2D attackHitbox;            // 공격 히트박스

    private void Awake()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    // 히트박스를 활성화하는 메서드
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    // 히트박스를 비활성화하는 메서드
    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    // 공격이 적과 충돌했을 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HK_Health enemy = other.GetComponent<HK_Health>();
            if (enemy != null)
            {
                // 공격한 위치를 적에게 전달
                enemy.TakeDamage(damage, transform.position);  // transform.position을 사용하여 공격 위치 전달
                Debug.Log($"Enemy {other.name} hit for {damage} damage!");
            }
        }
    }
}
