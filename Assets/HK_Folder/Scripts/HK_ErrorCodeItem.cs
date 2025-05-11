using UnityEngine;

public class HK_ErrorCodeItem : MonoBehaviour
{
    public float bounceForce = 5f;            // ó�� Ƣ����� ��
    public AudioClip pickupSound;

    private Rigidbody2D rb;
    private bool hasBounced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null && !hasBounced)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1.5f; // ���� �ӵ� ���� ����
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            hasBounced = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HK_PlayerInventory inventory = other.GetComponent<HK_PlayerInventory>();
            if (inventory != null)
            {
                inventory.AcquireErrorCode();
            }

            // ��Ż Ȱ��ȭ
            HK_Portal portal = Object.FindFirstObjectByType<HK_Portal>();
            if (portal != null)
            {
                portal.ActivatePortal();
            }

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}

