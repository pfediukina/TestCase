using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private List<SoundData> _sounds = new List<SoundData>();
    [SerializeField] private AudioSource _source;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(SoundTag tag)
    {
        var clip = _sounds.Where(p => p.Tag == tag).FirstOrDefault().Clip;
        if (clip != null)
        {
            _source.Stop();
            _source.clip = clip;
            _source.Play();
        }
    }
    
    public void PlayButtonSound()
    {
        var clip = _sounds.Where(p => p.Tag == SoundTag.BUTTON_SOUND).FirstOrDefault().Clip;
        if (clip != null)
        {
            _source.Stop();
            _source.clip = clip;
            _source.Play();
        }
    }
}

[Serializable]
public struct SoundData
{
    public SoundTag Tag;
    public AudioClip Clip;
}


public enum SoundTag
{
    POP_SOUND,
    BUY_SOUND,
    WIN_SOUND,
    LOSE_SOUND,
    COLLECT_SOUND,
    BUTTON_SOUND
}
