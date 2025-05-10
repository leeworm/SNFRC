using UnityEngine;

public class HK_EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float speed = 5f;
    public float lifeTime = 3f;
    public bool canPierce = false; // ���� ����
    public Vector2 direction = Vector2.right; // ����

    private void Start()
    {
        Destroy(gameObject, lifeTime);  // ���� �ð��� ������ �Ѿ��� �ڵ����� ����
    }

    private void Update()
    {
        // ������ �������� �Ѿ��� �̵�
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // �÷��̾�� �浹 ��
        {
            HK_PlayerHealth player = other.GetComponent<HK_PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);  // �÷��̾�� ������ ����
            }

            if (!canPierce)  // �������� ������ �Ѿ� ����
            {
                Destroy(gameObject);
            }
        }
        else if (!other.CompareTag("Enemy") && !other.isTrigger)  // ���� �浹���� ������, Ʈ���Ű� �ƴ� �ٸ� ��ü�� �浹 ��
        {
            if (!canPierce)  // �������� ������ �Ѿ� ����
            {
                Destroy(gameObject);
            }
        }
    }
}
