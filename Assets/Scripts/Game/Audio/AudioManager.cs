using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    #region Variable;

    public bool isMute = false;
    private float currentVolume;

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
                audioSource.volume = sound.volume * audioSource.volume;
                audioSource.loop = sound.isLoop;

                audioSource.Play();
                break;
            }
        }
    }

    public void StopEffect(string name)
    {
        foreach (AudioSource audioSource in EffectSources)
        {
            if (audioSource.isPlaying && audioSource.name == name)
            {
                audioSource.Stop();
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
        MusicSource.volume = sound.volume * MusicSource.volume;
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
        MusicSource.DOFade(currentVolume, 0.5f).SetUpdate(UpdateType.Normal, true);
    }

    public void FadeOutMusic()
    {
        currentVolume = MusicSource.volume;
        MusicSource.DOFade(0, 0.5f).SetUpdate(UpdateType.Normal, true);
    }

    public void PauseMusic()
    {
        MusicSource.Pause();
        foreach (AudioSource effectSource in EffectSources)
        {
            effectSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        MusicSource.UnPause();
        foreach (AudioSource effectSource in EffectSources)
        {
            effectSource.UnPause();
        }
    }

    public float GetMusicVolume()
    {
        return MusicSource.volume;
    }

    public float GetEffectVolume()
    {
        return EffectSources[0].volume;
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