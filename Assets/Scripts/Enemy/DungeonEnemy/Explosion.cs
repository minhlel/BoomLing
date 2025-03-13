using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    private Enemy creator; // Reference to the enemy that created this explosion
    [SerializeField] private EnemyAudio enemyAudio; // Must be assigned in Inspector
    // Method to set the creator
    public void SetCreator(Enemy creator)
    {
        this.creator = creator;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
        else if (collision.CompareTag("ThanhEnemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>(); // Get the specific enemy hit by the explosion
            if (enemy != null && enemy != creator) // Avoid damaging the creator
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    public void DestroyExplosion()
    {
        if (enemyAudio != null) // Safety check
        {
            enemyAudio.PlayExplosionSound();
        }
        Destroy(gameObject);
    }
}
