using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ
    [SerializeField] private int damageAmount = 15;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Tạo hiệu ứng nổ khi đạn va vào tường
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // Huỷ viên đạn
        Destroy(gameObject);
    }
}
