using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class duc_BasicEnemy : duc_eneemy
{

    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

}
