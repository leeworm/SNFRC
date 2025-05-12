using UnityEngine;
using System.Collections.Generic;

public class KH_SoundManager : MonoBehaviour
{
    private AudioSource sfxPlayer;
    public float masterVolumeSFX = 1f;
    
    [SerializeField] private AudioClip[] sfxAudioClips; 
    Dictionary<string, AudioClip> audioClipsDic = new Dictionary<string, AudioClip>();

    private static KH_SoundManager instance;
    public static KH_SoundManager Instance
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

        sfxPlayer = GetComponent<AudioSource>();
        
        foreach (AudioClip audioclip in sfxAudioClips)
        {
            if (!audioClipsDic.ContainsKey(audioclip.name))
            {
                audioClipsDic.Add(audioclip.name, audioclip);
            }
        }
    }

    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }
}
