using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSoundManager : MonoBehaviour
{
    public static HiddenSoundManager instance;

    [SerializeField][Range(0f, 1f)] private float SoundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float SoundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public HiddenSoundSource soundSourcePrefab;

    private void Awake()
    {
        instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        HiddenSoundSource obj = Instantiate(instance.soundSourcePrefab);
        HiddenSoundSource soundSource = obj.GetComponent<HiddenSoundSource>();
        soundSource.Play(clip, instance.SoundEffectVolume, instance.SoundEffectPitchVariance);
    }
}
