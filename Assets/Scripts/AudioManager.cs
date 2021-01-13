using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] songs;
    public static AudioManager SharedInstance;
    public string currentSong;

    void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
            Destroy(gameObject);

        if(SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds){
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;

            s.AudioSource.volume = s.volume;
            s.AudioSource.pitch = s.pitch;
            s.AudioSource.loop = s.loop;
        }
        foreach (Sound s in songs){
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;

            s.AudioSource.volume = s.volume;
            s.AudioSource.pitch = s.pitch;
            s.AudioSource.loop = s.loop;
        }
    }
    public void PlaySong (string newSong)
    {
        Sound s = Array.Find(songs, song => song.name == newSong);
        if (s == null || currentSong == newSong)
            return;

        foreach (Sound song in songs)
        {
            song.AudioSource.Stop();
        }
        s.AudioSource.Play();
        currentSong = newSong;
    }

    public void PlayClip (string newClip)
    {
        Sound s = Array.Find(sounds, sound => sound.name == newClip);
        if (s == null)
            return;
        else
            s.AudioSource.Play();
    }
}
