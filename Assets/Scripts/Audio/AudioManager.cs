using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    public AudioSource bgmSource;
    public GameObject effectSources;
    public AudioConfig audioConfig;
    
    // 主音量
    [Range(0,1)]
    private float mainVolume = 1;
    // 音效音量
    [Range(0,1)]
    private float soundEffectVolume = 1;
    // 背景音乐音量
    [Range(0,1)]
    private float backgroundMusicVolume = 1;
    
    
    private Tween tweenBgm;
    private Dictionary<string, AudioData> audioLibrary;
    
    public void Awake()
    {
        audioLibrary = new Dictionary<string, AudioData>();
        foreach (var data in audioConfig.audioLibrary)
        {
            audioLibrary.Add(data.key,data);
        }

        mainVolume = audioConfig.mainVolume;
        soundEffectVolume = audioConfig.soundEffectVolume;
        backgroundMusicVolume = audioConfig.backgroundMusicVolume;
    }

    private void DoVolumeTween(AudioSource source, float endVolume, float duration, Action onComplete = null)
    {
        if (tweenBgm != null)
            tweenBgm.Kill();
        if (source.volume != endVolume)
        {
            tweenBgm = DOTween.To(() => source.volume, x => source.volume = x, endVolume, duration);
            if (onComplete != null)
                tweenBgm.onComplete = new TweenCallback(onComplete);
        }
        else
        {
            onComplete?.Invoke();
        }
    }
    public void PlayBGM(string key,bool isLoop,bool isTransition,Action onComplete = null)
    {
        AudioData data;
        if (!audioLibrary.TryGetValue(key, out data))
        {
            Debug.LogError("音频：" + key + " 播放出错");
            return;
        }
        
        float volume = mainVolume * data.volume * backgroundMusicVolume;
        if (isTransition)
        {
            // 过渡播放
            if (!bgmSource.isPlaying)
            {
                // 当前没有播放bgm
                bgmSource.volume = 0;
                bgmSource.clip = data.clip;
                bgmSource.loop = isLoop;
                bgmSource.Play();
                DoVolumeTween(bgmSource, volume, 1.5f);
            }
            else
            {
                // 当前正在播放bgm，切歌 
                DoVolumeTween(bgmSource,0f,1.5f, () =>
                {
                    bgmSource.clip = data.clip;
                    bgmSource.loop = isLoop;
                    bgmSource.Play();
                    DoVolumeTween(bgmSource, volume, 1.5f);
                });
            }
        }
        else
        {
            // 硬切
            bgmSource.clip = data.clip;
            bgmSource.loop = isLoop;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
        
    }

    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    public void UnPauseBGM()
    {
        bgmSource.UnPause();
    }

    public void ChangeBGMVolume(float value)
    {
        bgmSource.volume = bgmSource.volume / backgroundMusicVolume * value;
        backgroundMusicVolume = value;
    }

    public void ResetBGMVolume()
    {
        bgmSource.volume = bgmSource.volume / backgroundMusicVolume * audioConfig.backgroundMusicVolume;
        backgroundMusicVolume = audioConfig.backgroundMusicVolume;
    }


    public AudioSource PlayEffect(string key,bool isLoop)
    {
        AudioData data;
        if (!audioLibrary.TryGetValue(key, out data))
        {
            Debug.LogError("音频：" + key + " 播放出错");
            return default;
        }
        float volume = mainVolume * data.volume * soundEffectVolume;
        var sources = effectSources.GetComponents<AudioSource>();
        // 是否找到空闲的source
        bool bTmp = false;
        for (int i = 0; i < sources.Length; i++)
        {
            if (!sources[i].isPlaying)
            {
                sources[i].clip = data.clip;
                sources[i].loop = isLoop;
                sources[i].volume = volume;
                bTmp = true;
                sources[i].Play();
                return sources[i];
            }
        }

        if (!bTmp)
        {
            var source = effectSources.AddComponent<AudioSource>();
            source.clip = data.clip;
            source.loop = isLoop;
            source.volume = volume;
            source.Play();
            return source;
        }
        return default;
    }
    
    public void StopBGM(bool isTransition,Action onComplete = null)
    {
        if (isTransition)
        {
            DoVolumeTween(bgmSource, 0f, 1.5f, () =>
            {
                bgmSource.Stop();
                bgmSource.clip = null;
                onComplete?.Invoke();
            });
        }
        else
        {
            bgmSource.Stop();
            bgmSource.clip = null;
            onComplete?.Invoke();
        }
    }

    public void StopEffect()
    {
        var sources = effectSources.GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].Stop();
        }
    }
}
