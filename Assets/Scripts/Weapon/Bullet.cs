using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float damage = 10f;


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Enemy") || collider2D.gameObject.CompareTag("Boss"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            EnemyHealth enemyHealth = collider2D.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        }
        else if (collider2D.gameObject.CompareTag("ThanhEnemy"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Enemy thanhEnemy = collider2D.GetComponent<Enemy>();
            if (thanhEnemy != null)
            {
                thanhEnemy.TakeDamage(damageAmount);
            }
            Destroy(gameObject);

        }
        else if (collider2D.gameObject.CompareTag("Wall"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (collider2D.gameObject.CompareTag("Duc_Enemy"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            duc_eneemy eneemy = collider2D.GetComponent<duc_eneemy>();
            if (eneemy != null)
            {
                eneemy.TakeDamge(damage);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);

    }




}
