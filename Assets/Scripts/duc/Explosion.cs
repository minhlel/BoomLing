using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int damage = 25;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        PlayerController player = collider2D.GetComponent<PlayerController>();
        duc_eneemy eneemy = collider2D.GetComponent<duc_eneemy>();
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            if (collider2D.CompareTag("Duc_Enemy"))
            {
                eneemy.TakeDamge(damage);
            }
        }
    }
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
