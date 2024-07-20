using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AudioClip))]
public class MusicManager : MonoBehaviour {

    public AudioSource Audio;
    public AudioClip BackgroundMusic;

    void Start() {
        this.Audio = this.GetComponent<AudioSource>();
        this.Audio.clip = this.BackgroundMusic;
        this.Audio.loop = true;
        this.Audio.Play();
    }

    void Update() {
        
    }
}
