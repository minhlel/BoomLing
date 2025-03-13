using UnityEngine;
using System.Reflection;
using System.Collections;

// Lớp GangsterHealth kế thừa từ EnemyHealth
public class GangsterHealth : EnemyHealth
{
    // Field để truy cập private field 'currentHealth' của EnemyHealth
    private FieldInfo currentHealthField;
    // Field để truy cập private field 'flash' của EnemyHealth
    private FieldInfo flashField;

    private void Awake()
    {
        // Lấy thông tin field 'currentHealth' trong EnemyHealth
        currentHealthField = typeof(EnemyHealth).GetField("currentHealth", BindingFlags.NonPublic | BindingFlags.Instance);
        // Lấy thông tin field 'flash'
        flashField = typeof(EnemyHealth).GetField("flash", BindingFlags.NonPublic | BindingFlags.Instance);
        if (flashField != null)
        {
            // Nếu chưa có Flash, thêm DummyFlash để tránh lỗi NullReferenceException
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
            // Khi máu ≤ 0, tự hủy enemy mà không gọi animation death
            if (health <= 0)
            {
                Debug.Log(gameObject.name + " đã chết! (GangsterHealth)");
                Destroy(gameObject);
            }
        }
    }
}

public class DummyFlash : Flash
{
    public new IEnumerator FlashRoutine()
    {
        yield break;
    }
}
