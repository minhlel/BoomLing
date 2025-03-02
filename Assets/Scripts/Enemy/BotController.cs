using System.Collections;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab; // Prefab của đạn
    [SerializeField] private GameObject bombPrefab;   // Prefab của bom

    [Header("Fire Point")]
    [SerializeField] private Transform firePoint;     // Điểm bắn/ném

    [Header("Settings")]
    [SerializeField] private float bulletSpeed = 10f; // Tốc độ của đạn
    [SerializeField] private float bombForce = 5f;    // Lực ném bom
    [SerializeField] private float actionInterval = 2f; // Thời gian giữa các hành động
    [SerializeField] private float switchActionTime = 5f; // Thời gian chuyển đổi hành động

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;    // Tốc độ di chuyển của bot
    [SerializeField] private Vector2 moveAreaMin;     // Góc dưới cùng bên trái của khu vực
    [SerializeField] private Vector2 moveAreaMax;     // Góc trên cùng bên phải của khu vực
    private Vector2 moveDirection;                   // Hướng di chuyển của bot
    private bool isFacingRight = true;

    [Header("Rotate Settings")]
    [SerializeField] private float rotateChance = 0.5f;

    private float actionTimer = 0f;                  // Bộ đếm thời gian cho hành động
    private bool isShooting = true;                  // Trạng thái hành động hiện tại (bắn hoặc ném)

    private void Start()
    {
        // Lặp lại hành động ngẫu nhiên
        InvokeRepeating(nameof(RandomAction), actionInterval, actionInterval);

        // Đặt hướng di chuyển ngẫu nhiên ban đầu
        moveDirection = GetRandomDirection();
    }

    private void Update()
    {
        MoveBot();
        UpdateActionState();
    }

    private void UpdateActionState()
    {
        // Tăng bộ đếm thời gian
        actionTimer += Time.deltaTime;

        // Kiểm tra xem có cần chuyển đổi trạng thái hành động hay không
        if (actionTimer >= switchActionTime)
        {
            isShooting = !isShooting; // Chuyển đổi giữa bắn và ném
            actionTimer = 0f;         // Đặt lại bộ đếm thời gian
        }
    }

    private void RandomAction()
    {
        if (isShooting)
        {
            ShootBullet();
        }
        else
        {
            ThrowBomb();
        }

        // Ngẫu nhiên quay trái hoặc quay phải
        RandomRotate();
    }

    private void MoveBot()
    {
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.deltaTime;

        if (newPosition.x < moveAreaMin.x || newPosition.x > moveAreaMax.x)
        {
            moveDirection.x = -moveDirection.x;
            newPosition.x = Mathf.Clamp(newPosition.x, moveAreaMin.x, moveAreaMax.x);
        }

        if (newPosition.y < moveAreaMin.y || newPosition.y > moveAreaMax.y)
        {
            moveDirection.y = -moveDirection.y;
            newPosition.y = Mathf.Clamp(newPosition.y, moveAreaMin.y, moveAreaMax.y);
        }

        transform.position = newPosition;

        if (Random.value < rotateChance)
        {
            RandomRotate();
        }
    }

    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void RandomRotate()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x = isFacingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        Vector3 firePointScale = firePoint.localPosition;
        firePointScale.x = isFacingRight ? Mathf.Abs(firePointScale.x) : -Mathf.Abs(firePointScale.x);
        firePoint.localPosition = firePointScale;
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (bullet != null)
        {
            Debug.Log("Bullet created: " + bullet.name); // Ghi log tên của đối tượng bullet
        }
        else
        {
            Debug.LogWarning("Bullet creation failed!"); // Báo động nếu đối tượng không được tạo
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log(bullet);
            rb.velocity = (isFacingRight ? firePoint.right : -firePoint.right) * bulletSpeed;
        }
        //Destroy(bullet, 2f);
    }

    private void ThrowBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 throwDirection = new Vector2(Random.Range(-0.5f, 0.5f), 1).normalized;
            throwDirection.x *= isFacingRight ? 1 : -1;
            rb.AddForce(throwDirection * bombForce, ForceMode2D.Impulse);
        }
        Destroy(bomb, 3f);
    }
}

