using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Names refences: Killed /")]
    [SerializeField] private Sound[] sounds;
    [SerializeField] public AudioSource audioSource;
    
    private Dictionary<string, AudioClip> Clips;
    
    [Serializable]
    public struct Sound
    {
        public string name;
        public AudioClip sound;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Clips = new Dictionary<string, AudioClip>();
        foreach (var sound in sounds)
        {
            Clips.Add(sound.name, sound.sound);
        }
    }

    public void PlaySound(string name)
    {
        audioSource.clip = Clips[name];
        audioSource.Play();
    }
}
