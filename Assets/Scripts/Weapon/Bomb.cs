using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Gây sát thương cho enemy (thêm logic tuỳ ý)
            Destroy(collision.gameObject); // Huỷ enemy
            Destroy(gameObject);
            // Tạo hiệu ứng nổ
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Tạo hiệu ứng nổ khi đạn va vào tường
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        // Huỷ bom sau khi va chạm
        Destroy(gameObject);
    }
}
