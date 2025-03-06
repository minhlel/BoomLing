using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ
    [SerializeField] private int damageAmount = 1;
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            PlayerHealth playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
        else if (collider2D.gameObject.CompareTag("Wall"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Tạo hiệu ứng nổ khi đạn va vào tường
            Destroy(gameObject);
        }
        // Huỷ viên đạn
        //Destroy(gameObject);
    }
}
