using UnityEngine;

public class HK_BassRapidShotController : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10f;
    public float lifeTime = 2f;

    [HideInInspector] public GameObject shooter; // �߻��� ����

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ڽŰ� �浹�ϸ� ����
        if (collision.gameObject == shooter)
            return;

        if (collision.CompareTag("Player"))
        {
            HK_Player player = collision.GetComponent<HK_Player>();
            if (player != null)
            {
                player.TakeDamage(damage); // �÷��̾� ������
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

