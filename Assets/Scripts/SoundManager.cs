using System;
using UnityEngine;

//sfx by kenney.nl

// music made on beepBox by NaejDoree
// music link
// https://www.beepbox.co/#9n31s0k0l00e0at2-a7g0fj07r1i0o432T0v5u00f0q0x10n53d3aw5h2E2b706T5v1u05f60lc2dd2j02e02fd178q8143d26HK_Sziiirrqih99h0E0T1v4u01f0q0w10p5d4aA5F4B0Q0248Pac74E3b8628635T2v4u15f10w4qw02d03w0E0b4h404w4w000000000000014i4w8200000h4g414g000p21YISLY6bSq_13bWHHKIfCkbYP2-KOdCzXLcI2suCzS21bUeGGGGGsvjinRkkQOdcU-KILjnZpdtypuq0mq_Ucsvh9N-yhNZ4O8pvno5CzeMCz98W2ewLFF8Waqiew0
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;

    [SerializeField] private AudioClip _endTurnClip;
    [SerializeField] private AudioClip _executionClip;
    [SerializeField] private AudioClip _cardPickupClip;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public static void PlayEndTurnSFX()
    {
        PlaySFX(Instance._endTurnClip);
    }
    public static void PlayCardPickUpFX()
    {
        PlaySFX(Instance._cardPickupClip);
    }
    public static void PlayExecutionSFX()
    {
        PlaySFX(Instance._executionClip);
    }
    
    
    public static void PlaySFX(AudioClip clip)
    {
        Instance._sfxAudioSource.PlayOneShot(clip);
    }
}
