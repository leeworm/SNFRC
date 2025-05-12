using UnityEngine;

public class HK_ErrorCodeItem : MonoBehaviour
{
    [Header("Physics Settings")]
    public float bounceForce = 5f;

    [Header("Sound Settings")]
    public AudioClip pickupSound;
    [Range(0f, 1f)] public float pickupVolume = 0.8f;

    [Header("Portal Settings")]
    public GameObject portalPrefab; // 포탈 프리팹을 인스펙터에서 할당할 변수
    public Transform portalSpawnPosition; // 포탈이 소환될 위치

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

        // 1. 인벤토리 처리
        var inventory = other.GetComponent<HK_PlayerInventory>();
        inventory?.AcquireErrorCode();

        // 2. 포탈 소환 (프리팹을 인스턴스화하여 소환)
        if (portalPrefab != null && portalSpawnPosition != null)
        {
            Instantiate(portalPrefab, portalSpawnPosition.position, Quaternion.identity);
        }

        // 3. 사운드 재생 (볼륨 조절 포함)
        PlayPickupSound();

        // 4. 아이템 제거
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

        Destroy(tempAudio, pickupSound.length); // 사운드 끝나면 제거
    }
}
