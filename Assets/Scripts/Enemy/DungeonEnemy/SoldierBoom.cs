using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBoom : Enemy
{
    [SerializeField] private GameObject explosionPrefabs;
    private void  CreateExplosion() 
    {
        if (explosionPrefabs != null)
        {
            GameObject explosion = Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
            Explosion explosionScript = explosion.GetComponent<Explosion>();
            if (explosionScript != null)
            {
                explosionScript.SetCreator(this);

                // Add and configure EnemyAudio if not present
                EnemyAudio enemyAudio = explosion.GetComponent<EnemyAudio>();
                if (enemyAudio == null)
                {
                    enemyAudio = explosion.AddComponent<EnemyAudio>();
                    enemyAudio.effectAudioSource = explosion.AddComponent<AudioSource>(); // Add AudioSource
                    enemyAudio.explosion = Resources.Load<AudioClip>("explosion"); // Load explosion clip (adjust path)
                }
            }
        }
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            CreateExplosion();
            Die();
        }
    }
}
