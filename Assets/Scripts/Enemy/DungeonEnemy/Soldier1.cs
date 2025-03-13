using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier1 : Enemy
{
    private void Awake()
    {
        gameObject.tag = "ThanhEnemy";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(enterDamage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(stayDamage);
        }
    }
}
