using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] public AudioSource effectAudioSource;
    [SerializeField] public AudioClip explosion;
    [SerializeField] public AudioClip enemyDeath;

    public void PlayExplosionSound() 
    {
        effectAudioSource.PlayOneShot(explosion);
    }
    public void PlayEnemyDeathSound() 
    {
        effectAudioSource.PlayOneShot(enemyDeath);
    }
}
