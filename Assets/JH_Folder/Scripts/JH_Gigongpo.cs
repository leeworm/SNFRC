using UnityEngine;

public class JH_Gigongpo : MonoBehaviour
{
    public float speed = 5f;    //�̻��� �ӵ�
    public float lifeTime = 1.1f; //�̻��� ���� �ð�
    private Vector2 direction;  //�̻��� �̵� ����
    private bool hasHit = false; // �浹 ���θ� üũ�ϴ� �÷���

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        if (!hasHit) // �浹���� �ʾ��� ���� �̵�
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �̹� �浹�ߴٸ� ����
        if (hasHit) return;

        JH_Hurtbox hurtBox = other.GetComponent<JH_Hurtbox>();

        if (hurtBox != null)
        {
            if (hurtBox.ownerEntity != null &&
                (hurtBox.ownerEntity.CompareTag("Enemy") || hurtBox.ownerEntity.CompareTag("Player")))
            {
                hasHit = true; // �浹 �÷��� ����


                // ����� ����
                Destroy(gameObject);
            }
        }
    }
}