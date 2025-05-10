using UnityEngine;

public class HK_PlayerAttack : MonoBehaviour
{
    public int damage = 10;                   // ���ݷ�
    public Collider2D attackHitbox;          // ���� ������ �ݶ��̴� (Trigger)

    private void Awake()
    {
        // ���� �ÿ� ��Ȱ��ȭ (�� ���̰�)
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    /// <summary>
    /// ���� ���� �� ȣ�� (��: �ִϸ��̼� �̺�Ʈ���� ȣ��)
    /// </summary>
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    /// <summary>
    /// ���� ������ �� ȣ��
    /// </summary>
    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hitbox�� Trigger�� �浹���� �� �����
        if (other.CompareTag("Enemy"))
        {
            HK_Health enemy = other.GetComponent<HK_Health>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Enemy {other.name} hit for {damage} damage!");
            }
        }
    }
}
