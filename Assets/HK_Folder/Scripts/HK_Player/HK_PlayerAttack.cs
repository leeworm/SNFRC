using UnityEngine;

public class HK_PlayerAttack : MonoBehaviour
{
    public int damage = 10;                    // ���ݷ�
    public Collider2D attackHitbox;            // ���� ��Ʈ�ڽ�

    private void Awake()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    // ��Ʈ�ڽ��� Ȱ��ȭ�ϴ� �޼���
    public void EnableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = true;
    }

    // ��Ʈ�ڽ��� ��Ȱ��ȭ�ϴ� �޼���
    public void DisableHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.enabled = false;
    }

    // ������ ���� �浹���� �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            HK_Health enemy = other.GetComponent<HK_Health>();
            if (enemy != null)
            {
                // ������ ��ġ�� ������ ����
                enemy.TakeDamage(damage, transform.position);  // transform.position�� ����Ͽ� ���� ��ġ ����
                Debug.Log($"Enemy {other.name} hit for {damage} damage!");
            }
        }
    }
}
