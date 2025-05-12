using UnityEngine;

public class HK_ErrorCodeItem : MonoBehaviour
{
    [Header("Physics Settings")]
    public float bounceForce = 5f;

    [Header("Sound Settings")]
    public AudioClip pickupSound;
    [Range(0f, 1f)] public float pickupVolume = 0.8f;

    [Header("Portal Settings")]
    public GameObject portalPrefab; // ��Ż �������� �ν����Ϳ��� �Ҵ��� ����
    public Transform portalSpawnPosition; // ��Ż�� ��ȯ�� ��ġ

    private Rigidbody2D rb;
    private bool hasBounced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null && !hasBounced)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1.5f;
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            hasBounced = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        MainGameManager.Instance.GetErrorPiece();

        // 1. �κ��丮 ó��
        var inventory = other.GetComponent<HK_PlayerInventory>();
        inventory?.AcquireErrorCode();

        // 2. ��Ż ��ȯ (�������� �ν��Ͻ�ȭ�Ͽ� ��ȯ)
        if (portalPrefab != null && portalSpawnPosition != null)
        {
            Instantiate(portalPrefab, portalSpawnPosition.position, Quaternion.identity);
        }
        HK_GameManager.Instance.CreatePotal();

        // 3. ���� ��� (���� ���� ����)
        PlayPickupSound();

        // 4. ������ ����
        Destroy(gameObject);
    }

    private void PlayPickupSound()
    {
        if (pickupSound == null) return;

        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.clip = pickupSound;
        source.volume = pickupVolume;
        source.Play();

        Destroy(tempAudio, pickupSound.length); // ���� ������ ����
    }
}
