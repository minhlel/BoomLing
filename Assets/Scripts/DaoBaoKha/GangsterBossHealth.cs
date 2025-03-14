using UnityEngine;
using System.Reflection;
using System.Collections;

// Lớp GangsterBossHealth kế thừa từ EnemyHealth
public class GangsterBossHealth : EnemyHealth
{
    // Field để truy cập private field 'currentHealth' của EnemyHealth
    private FieldInfo currentHealthField;
    // Field để truy cập private field 'flash' của EnemyHealth (dù không dùng)
    private FieldInfo flashField;

    private void Awake()
    {
        // Lấy thông tin field 'currentHealth' trong EnemyHealth
        currentHealthField = typeof(EnemyHealth).GetField("currentHealth", BindingFlags.NonPublic | BindingFlags.Instance);
        // Lấy thông tin field 'flash' (dù không dùng flash)
        flashField = typeof(EnemyHealth).GetField("flash", BindingFlags.NonPublic | BindingFlags.Instance);
        if (flashField != null)
        {
            // Thêm DummyFlash để tránh lỗi NullReferenceException
            Flash dummy = GetComponent<DummyFlash>();
            if (dummy == null)
            {
                dummy = gameObject.AddComponent<DummyFlash>();
            }
            flashField.SetValue(this, dummy);
        }
    }

    private void Update()
    {
        // Kiểm tra giá trị currentHealth thông qua Reflection
        if (currentHealthField != null)
        {
            int health = (int)currentHealthField.GetValue(this);
            // Khi máu ≤ 0, thông báo cho EndGameManager và hủy
            if (health <= 0)
            {
                Debug.Log(gameObject.name + " đã chết! (GangsterBossHealth)");
                if (EndGameManager.Instance != null)
                {
                    EndGameManager.Instance.BossKilled(); // Thông báo để chuyển scene
                }
                Destroy(gameObject);
            }
        }
    }
}

