using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAudioType
{
    BGM,
    Effect,
}

[Serializable]
public struct AudioData
{
    // 检索key
    public string key;
    // 音频文件
    public AudioClip clip;
    // 音频音量
    [Range(0,1)]
    public float volume;
    // 音频类型
    public EAudioType type;
}

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Mira/AudioConfig", order = 1)]
public class AudioConfig : ScriptableObject
{
    public string audioPath = "Audio/";
    // 主音量
    [Range(0,1)]
    public float mainVolume = 1;
    // 音效音量
    [Range(0,1)]
    public float soundEffectVolume = 1;
    // 背景音乐音量
    [Range(0,1)]
    public float backgroundMusicVolume = 1;
    
    public List<AudioData> audioLibrary;

}
