using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------------Audio Source--------------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("--------------------Audio Clip--------------------")]
    public AudioClip background;
    public AudioClip deathEnemy;
    public AudioClip gunPlayer;
    public AudioClip gunEnemy;
    public AudioClip deathPlayer;
    public AudioClip boom;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
