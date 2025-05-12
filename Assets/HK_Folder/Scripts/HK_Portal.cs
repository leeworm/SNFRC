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
        spriteRenderer = GetComponent<SpriteRenderer>(); // ��Ż�� SpriteRenderer
        spriteRenderer.enabled = false; // �ʱ� ���¿����� ��Ż�� ������ ����
    }

    public void ActivatePortal()
    {
        if (isActivated) return;
        isActivated = true;
        spriteRenderer.enabled = true; // ��Ż ���̰� ����
        StartPortalAnimation();  // ��Ż �ִϸ��̼� ����
    }

    private void StartPortalAnimation()
    {
        // �ִϸ��̼��� ������ �׳� �ڷ�ƾ���� �ִϸ��̼� ȿ���� ���� ���� �ֽ��ϴ�.
        if (portalCoroutine != null)
            StopCoroutine(portalCoroutine);

        portalCoroutine = StartCoroutine(PortalOpenAnimation());
    }

    private IEnumerator PortalOpenAnimation()
    {
        // ��Ż �ִϸ��̼� ȿ��
        // ��: ���� �ִϸ��̼�, ũ�� ��ȭ ��
        float t = 0f;
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = initialScale * 1.5f; // ���÷� ũ�� Ȯ��

        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // �ִϸ��̼��� ������ ��Ż�� ���� �̵� �����ϰ� �����
        EnablePortalCollider();
    }

    private void EnablePortalCollider()
    {
        // ��Ż�� Ȱ��ȭ�ǰ�, �÷��̾ �浹�� �� �ֵ��� �ݶ��̴� Ȱ��ȭ
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
