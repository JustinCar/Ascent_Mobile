using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip[] music;

    AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();

        // Play a random music clip
        if (!audio.playOnAwake)
        {
            audio.clip = music[Random.Range(0, music.Length)];
            audio.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {

        // When current music clip ends,
        // Play a random music clip
        if (!audio.isPlaying)
        {
            audio.clip = music[Random.Range(0, music.Length)];
            audio.Play();
        }
	}
}
