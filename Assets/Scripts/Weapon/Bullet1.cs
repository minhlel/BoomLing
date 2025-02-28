using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Thêm logic sát thương ở đây
        Debug.Log("Đạn va chạm với: " + collision.gameObject.name);

        // Huỷ đạn sau khi va chạm
        Destroy(gameObject);
    }
}
