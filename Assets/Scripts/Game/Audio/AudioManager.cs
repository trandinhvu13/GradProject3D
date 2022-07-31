using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    #region Variable;

    public bool isMute = false;

    #endregion
    #region Components
    
    public List<AudioSource> EffectSources;
    public AudioSource MusicSource;

    #endregion
    #region Audio Clips

    [SerializeField] private Sound[] audioLibrary;

    #endregion
    #region Method

    public void PlayEffect(string name, bool isUnique = false)
    {
        Sound sound = Array.Find(audioLibrary, s => s.name == name);
        if (sound == null) return;

        if (isUnique)
        {
            foreach (AudioSource audioSource in EffectSources)
            {
                if (audioSource.isPlaying && audioSource.name == name) return;
            }
        }
        foreach (AudioSource audioSource in EffectSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.name = sound.name;
                audioSource.clip = sound.clip;
                audioSource.volume = sound.volume;
                audioSource.loop = sound.isLoop;
                
                audioSource.Play();
                break;
            }
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(audioLibrary, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound named {name} not found!");
            return;
        }

        MusicSource.name = sound.name;
        MusicSource.clip = sound.clip;
        MusicSource.volume = sound.volume;
        MusicSource.loop = sound.isLoop;
        
        MusicSource.Play();
    }

    public void MuteAll()
    {
        isMute = true;
        
        foreach (AudioSource audioSource in EffectSources)
        {
            audioSource.mute = true;
        }
        
        MusicSource.mute = true;
    }

    public void UnmuteAll()
    {
        isMute = false;
        
        foreach (AudioSource audioSource in EffectSources)
        {
            audioSource.mute = false;
        }
        
        MusicSource.mute = false;
    }

    public void MuteEffectSound(string name)
    {
        foreach (AudioSource audioSource in EffectSources)
        {
            if (audioSource.clip == null) continue;
            if (audioSource.name == name)
            {
                audioSource.mute = true;
            }
        }
    }
    
    public void UnmuteEffectSound(string name)
    {
        foreach (AudioSource audioSource in EffectSources)
        {
            if (audioSource.clip == null) continue;
            if (audioSource.name == name)
            {
                audioSource.mute = false;
            }
        }
    }

    public void ChangeVolumeMusic(float amount)
    {
        MusicSource.volume = amount;
    }

    public void ChangeVolumeEffect(float amount)
    {
        foreach (AudioSource effectSource in EffectSources)
        {
            effectSource.volume = amount;
        }
    }

    public void FadeInMusic()
    {
        MusicSource.DOFade(1, 0.5f).SetUpdate(UpdateType.Normal,true);
    }

    public void FadeOutMusic()
    {
        MusicSource.DOFade(0, 0.5f).SetUpdate(UpdateType.Normal,true);
    }
    #endregion

    protected override void InternalInit()
    {
    }

    protected override void InternalOnDestroy()
    {
    }

    protected override void InternalOnDisable()
    {
    }

    protected override void InternalOnEnable()
    {
    }
}