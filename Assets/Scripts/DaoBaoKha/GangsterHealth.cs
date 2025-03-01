using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangsterHealth : MonoBehaviour
{
    public Transform healthBar;  // GameObject của thanh máu (Sprite)
    public float health = 100f;
    public float maxHealth = 100f;

    void Update()
    {
        // Tính tỷ lệ máu còn lại
        float healthRatio = Mathf.Clamp(health / maxHealth, 0, 1);

        // Cập nhật chiều rộng của thanh máu
        healthBar.localScale = new Vector3(healthRatio, 1, 1);

        // Nếu hết máu, hủy boss
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Hàm để trừ máu
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
