using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(AudioClip))]
public class MusicManager : MonoBehaviour {

    public static MusicManager Instance;

    public AudioSource Audio;
    public AudioSource AudioFX;
    public AudioClip BackgroundMusic;

    public AudioClip ImpactSound;
    public AudioClip DeathSound;
    public AudioClip PowerSound;
    public AudioClip CaptureSound;

    void Start() {
        Instance = this;

        this.Audio = this.GetComponent<AudioSource>();
        this.Audio.clip = this.BackgroundMusic;
        this.Audio.loop = true;
        this.Audio.Play();
    }

    public void PlayImpactSound() {
        this.AudioFX.clip = this.ImpactSound;
        this.AudioFX.Play();
    }

    public void PlayDeathSound() {
        this.AudioFX.clip = this.DeathSound;
        this.AudioFX.Play();
    }

    public void PlayPowerUpSound() {
        this.AudioFX.clip = this.PowerSound;
        this.AudioFX.Play();
    }

    public void PlayCaptureSound() {
        this.AudioFX.clip = this.CaptureSound;
        this.AudioFX.Play();
    }

    void Update() {
        
    }
}
