using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Sources
    public AudioSource musicSource;
    public AudioSource sfxSource;


    void Awake()
    {
        instance = this;

        SetVolumes();
    }

    public void SetVolumes()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 100) / 100;   // / 100 because audiosource volume is normalized
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 100) / 100;   // *
    }
}
