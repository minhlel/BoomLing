using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect; // Hiệu ứng nổ

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            // Gây sát thương cho enemy (thêm logic tuỳ ý)
            Destroy(collider2D.gameObject); // Huỷ enemy
            Destroy(gameObject);
            // Tạo hiệu ứng nổ
        }
        else if (collider2D.gameObject.CompareTag("Wall"))
        {
            // Tạo hiệu ứng nổ khi đạn va vào tường
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // Huỷ viên đạn
        Destroy(gameObject);
    }
}
