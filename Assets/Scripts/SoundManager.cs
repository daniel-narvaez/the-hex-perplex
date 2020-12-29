using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip _audioClip;

        [Range(0f,1)]
        public float volume;
        [Range(.1f,3)]
        public float pitch;
        public bool loop;
    }
    AudioSource _audioSource;
    public List<Sound> sounds;
    public string currentClip;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeAudioClip(string newClip)
    {
        sounds.Find(clip => clip.name == newClip);
        
    }
}
