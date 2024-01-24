using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVoices : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private AudioClip[] audios;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();

        int randomChoice = Random.Range(0, 3);
        _audioSource.clip = audios[randomChoice];

    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            _audioSource.Play();
            delay = 999999f;
        }
    }
}
