using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private static BackgroundMusic instance;
    public static BackgroundMusic Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BGM_Change()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
