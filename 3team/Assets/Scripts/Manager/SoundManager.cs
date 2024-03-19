using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public AudioSource BGM { get; set; }
    public AudioSource Effect { get; set; }
    public AudioSource Effect2 { get; set; }
    public AudioSource Effect3 { get; set; }
    public void Awake()
    {
        BGM = gameObject.AddComponent<AudioSource>();
        Effect = gameObject.AddComponent<AudioSource>();  
        Effect2 = gameObject.AddComponent<AudioSource>();
        Effect3 = gameObject.AddComponent<AudioSource>();
    }

    public void BGMPlay(string sound)
    {
        BGM.Stop();
        BGM.clip = Manager.Resources.LoadAudioClip(sound);
        BGM.Play();
    }

    public void EffectPlay(string sound)
    {
        if(!Effect.isPlaying)
        {
            Effect.clip = Manager.Resources.LoadAudioClip(sound);
            Effect.Play();
            return;
        }

        if(!Effect2.isPlaying) 
        {
            Effect2.clip = Manager.Resources.LoadAudioClip(sound);
            Effect2.Play();
            return;
        }

        if (!Effect3.isPlaying)
        {
            Effect3.clip = Manager.Resources.LoadAudioClip(sound);
            Effect3.Play();
            return;
        }
    }
}
