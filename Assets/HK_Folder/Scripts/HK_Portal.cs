using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HK_Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    public string nextSceneName;
    public AudioClip warpSound;
    [Range(0f, 1f)] public float warpVolume = 0.8f;

    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;
    private Coroutine portalCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 포탈의 SpriteRenderer
        spriteRenderer.enabled = false; // 초기 상태에서는 포탈이 보이지 않음
    }

    public void ActivatePortal()
    {
        if (isActivated) return;
        isActivated = true;
        spriteRenderer.enabled = true; // 포탈 보이게 설정
        StartPortalAnimation();  // 포탈 애니메이션 시작
    }

    private void StartPortalAnimation()
    {
        // 애니메이션이 없으면 그냥 코루틴으로 애니메이션 효과를 만들 수도 있습니다.
        if (portalCoroutine != null)
            StopCoroutine(portalCoroutine);

        portalCoroutine = StartCoroutine(PortalOpenAnimation());
    }

    private IEnumerator PortalOpenAnimation()
    {
        // 포탈 애니메이션 효과
        // 예: 투명도 애니메이션, 크기 변화 등
        float t = 0f;
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = initialScale * 1.5f; // 예시로 크기 확대

        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // 애니메이션이 끝나면 포탈을 통해 이동 가능하게 만들기
        EnablePortalCollider();
    }

    private void EnablePortalCollider()
    {
        // 포탈이 활성화되고, 플레이어가 충돌할 수 있도록 콜라이더 활성화
        Collider2D portalCollider = GetComponent<Collider2D>();
        if (portalCollider != null)
        {
            portalCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated || !other.CompareTag("Player")) return;

        PlayWarpSound();
        LoadNextScene();
    }

    private void PlayWarpSound()
    {
        if (warpSound == null) return;

        GameObject temp = new GameObject("WarpSound");
        AudioSource source = temp.AddComponent<AudioSource>();
        source.clip = warpSound;
        source.volume = warpVolume;
        source.Play();
        Destroy(temp, warpSound.length);
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set on HK_Portal.");
        }
    }
}
