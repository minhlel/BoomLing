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
    // [SerializeField] private float switchActionTime = 5f; // Thời gian chuyển đổi hành động

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;    // Tốc độ di chuyển của bot
    [SerializeField] private Vector2 moveAreaMin;     // Góc dưới cùng bên trái của khu vực
    [SerializeField] private Vector2 moveAreaMax;     // Góc trên cùng bên phải của khu vực
    private Vector2 moveDirection;                   // Hướng di chuyển của bot
    private bool isFacingRight = true;

    [Header("Rotate Settings")]
    [SerializeField] private float rotateChance = 0.5f;

    // private float actionTimer = 0f;                  // Bộ đếm thời gian cho hành động
    // private bool isShooting = true;                  // Trạng thái hành động hiện tại (bắn hoặc ném)
    private Transform bossPosition;
    private AudioManager SFX;
    private void Start()
    {
        SFX = FindObjectOfType<AudioManager>();
        bossPosition = GetComponent<Transform>();
        // Lặp lại hành động ngẫu nhiên
        InvokeRepeating(nameof(RandomAction), actionInterval, actionInterval);
        SFX.PlaySFX(SFX.gunEnemy);
        // Đặt hướng di chuyển ngẫu nhiên ban đầu
        moveDirection = GetRandomDirection();
    }
    // private void Awake()
    // {
    //     SFX = FindObjectOfType<AudioManager>();
    //     bossPosition = GetComponent<Transform>();
    //     // Lặp lại hành động ngẫu nhiên
    //     InvokeRepeating(nameof(RandomAction), actionInterval, actionInterval);

    //     // Đặt hướng di chuyển ngẫu nhiên ban đầu
    //     moveDirection = GetRandomDirection();
    // }
    private void Update()
    {
        MoveBot();
        //UpdateActionState();
    }

    // private void UpdateActionState()
    // {
    //     // Tăng bộ đếm thời gian
    //     actionTimer += Time.deltaTime;

    //     // Kiểm tra xem có cần chuyển đổi trạng thái hành động hay không
    //     if (actionTimer >= switchActionTime)
    //     {
    //         isShooting = !isShooting; // Chuyển đổi giữa bắn và ném
    //         actionTimer = 0f;         // Đặt lại bộ đếm thời gian
    //     }
    // }

    private void RandomAction()
    {
        // if (isShooting)
        // {
        //     AudioManager.Instance.PlaySFX(AudioManager.Instance.gunEnemy);
        //     ShootBullet();
        // }
        // else
        // {
        //     ThrowBomb();
        // }
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.gunEnemy);
        ShootBullet();
        ThrowBomb();
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
        firePoint.localScale = firePointScale;

        // Nếu firePoint cần di chuyển để phù hợp với góc nhìn, điều chỉnh vị trí của nó
        Vector3 spawnPosition = firePoint.position;
        if (isFacingRight)
        {
            // Đổi vị trí x của firePoint sang đối xứng
            spawnPosition.x = bossPosition.transform.position.x - (firePoint.position.x - bossPosition.transform.position.x);
        }
    }

    private void ShootBullet()
    {
        int bulletCount = 4; // Số lượng viên đạn
        float spreadAngle = 30f; // Góc lệch giữa các viên đạn (độ)
        float angleStep = spreadAngle / (bulletCount - 1); // Khoảng cách giữa các góc
        float startAngle = -spreadAngle / 2; // Góc bắt đầu bắn

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Tính góc của viên đạn
                float angle = startAngle + i * angleStep;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * (isFacingRight ? firePoint.right : -firePoint.right);

                // Đặt vận tốc cho viên đạn
                rb.velocity = direction * bulletSpeed;
            }
            //Destroy(bullet, 2f);
        }
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

