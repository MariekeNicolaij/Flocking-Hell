using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Audio
    public List<AudioClip> musicClips;
    public List<SFXClip> sfxClips;

    // Sources
    public AudioSource musicSource;
    public float sfxVolume;


    void Awake()
    {
        instance = this;

        // Set clips
        if (musicClips.Count == 0)
            musicClips = new List<AudioClip>(Resources.LoadAll<AudioClip>("Audio/Music"));

        SetVolumes();

        if (SceneManager.GetActiveScene().name == "Game")
            PlayMusic();
    }

    public void SetVolumes()
    {
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 75) / 100;   // / 100 because audiosource volume is normalized
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 100) / 100;   // *
    }

    public void PlaySound(AudioSource source, Sounds sound)
    {
        AudioClip clip = null;

        // Find sound in list
        foreach (SFXClip sfxClip in sfxClips)
            if (sfxClip.type == sound)
            {
                clip = sfxClip.clip;
                break;
            }

        // No clip no sound
        if (clip == null)
            return;

        // Set volume
        source.volume = sfxVolume;
        // Play the sound
        source.clip = clip;
        source.Play();
    }

    public void PlayMusic()
    {
        if (musicClips.Count == 0)
            return;

        musicSource.clip = musicClips[Random.Range(0, musicClips.Count - 1)]; // -1 because index
        musicSource.Play();

        Invoke("PlayMusic", musicSource.clip.length);
    }
}

[System.Serializable]
public class SFXClip
{
    public Sounds type;
    public AudioClip clip;
}

public enum Sounds
{
    AIDies,
    AIFlies,
    BulletHit,
    PlayerDies,
    Select,
    Shoot,
    Walk
}
