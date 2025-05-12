using UnityEngine;

public class B_AudioManager : MonoBehaviour
{
    public static B_AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip defaultBGM;

    [Header("효과음 클립들")]
    public AudioClip[] footstepClips;   // 4개 넣기
    public AudioClip hurtClip;          // 맞는 소리

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (defaultBGM != null)
            PlayBGM(defaultBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // 👣 랜덤 발소리
    public void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        sfxSource.PlayOneShot(footstepClips[index]);
    }

    // 💢 피격 사운드
    public void PlayHurt()
    {
        if (hurtClip != null)
            sfxSource.PlayOneShot(hurtClip);
    }
}
