using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class KH_SoundManager : MonoBehaviour
{
    private AudioSource sfxPlayer;
    public float masterVolumeSFX = 1f;

    bool isPlayingEffect = false;
    
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

    public void PlaySFXSound_B(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
            return;

        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    }


    public void PlaySFXSound(string name, float volume = 1f)
    {
        if (audioClipsDic.ContainsKey(name) == false)
            return;

        if (isPlayingEffect) return; // 중복 방지

        StartCoroutine(PlayWithCooldown(name, volume));
    }

    IEnumerator PlayWithCooldown(string name, float volume)
    {
        isPlayingEffect = true;
    
        sfxPlayer.PlayOneShot(audioClipsDic[name], volume * masterVolumeSFX);
    
        yield return new WaitForSeconds(0.1f); // 쿨타임
        isPlayingEffect = false;
    }

}
