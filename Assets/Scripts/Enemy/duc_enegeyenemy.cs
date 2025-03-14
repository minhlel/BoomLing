using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duc_enegeyenemy : duc_eneemy
{


        [SerializeField] private int damageAmount = 10;
        [SerializeField] private GameObject energryObject;

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

    protected override void Die()
    {
        if (energryObject != null)
        {
            GameObject energry = Instantiate(energryObject, transform.position, Quaternion.identity);
            Destroy(energry, 5f);
        }
        base.Die();
    }
}
