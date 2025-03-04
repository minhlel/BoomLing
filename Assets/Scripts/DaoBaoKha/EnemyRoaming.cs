using System.Collections;
using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    public enum State { Idle, Walk, Run, Attack }
    private State state = State.Walk; // Nếu không có player, enemy sẽ mặc định tuần tra (Walk)

    [Header("Cài đặt Tuần tra")]
    [SerializeField] private float roamInterval = 2f;   // Thời gian giữ hướng ngẫu nhiên
    [SerializeField] private float walkSpeed = 2f;        // Tốc độ di chuyển khi tuần tra

    [Header("Cài đặt Đuổi theo")]
    [SerializeField] private float detectionRange = 10f;  // Khoảng cách phát hiện player
    [SerializeField] private float runSpeed = 4f;         // Tốc độ chạy khi đuổi theo player

    [Header("Cài đặt Tấn công")]
    [SerializeField] private float attackRange = 2f;      // Khoảng cách kích hoạt tấn công
    [SerializeField] private float attackCooldown = 2f;   // Thời gian chờ giữa các đợt tấn công
    [SerializeField] private int attackDamage = 10;       // Sát thương mỗi đợt tấn công

    [Header("Tham chiếu")]
    // Không gán trực tiếp, enemy sẽ tự tìm đối tượng có tag "Player"
    private Transform player;
    public Animator animator;       // Animator của enemy

    private Rigidbody2D rb;
    private Vector2 moveDir;        // Hướng di chuyển hiện tại
    private float attackTimer = 0f; // Bộ đếm cooldown cho tấn công

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Walk;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private void Update()
    {
        // Tự tìm đối tượng player theo tag "Player" nếu chưa có
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Nếu có player, chuyển trạng thái dựa trên khoảng cách
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist <= detectionRange)
            {
                if (dist > attackRange)
                {
                    state = State.Run; // Chuyển sang chạy (Run) khi phát hiện player nhưng chưa đủ gần để tấn công
                }
                else
                {
                    state = State.Attack; // Nếu gần, chuyển sang trạng thái tấn công
                }
            }
            else
            {
                state = State.Walk; // Nếu không trong tầm phát hiện, quay lại tuần tra (Walk)
            }
        }
        else
        {
            state = State.Walk;
        }

        // Xử lý trạng thái tấn công: giảm bộ đếm và gọi hàm tấn công khi cooldown hết
        if (state == State.Attack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                //AttackPlayer();
                attackTimer = attackCooldown;
            }
        }

        // Cập nhật các parameter cho Animator dựa trên trạng thái
        switch (state)
        {
            case State.Idle:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                break;
            case State.Walk:
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                break;
            case State.Run:
                animator.SetBool("IsWalking", false);   
                animator.SetBool("IsRunning", true);
                break;
            case State.Attack:
                // Trong trạng thái Attack, chúng ta sẽ dùng Trigger để kích hoạt animation tấn công.
                // (Trigger được gọi ngay trong hàm AttackPlayer)
                break;
        }
    }

    private void FixedUpdate()
    {
        // Di chuyển tùy theo trạng thái
        if (state == State.Walk)
        {
            rb.MovePosition(rb.position + moveDir * walkSpeed * Time.fixedDeltaTime);
        }
        else if (state == State.Run)
        {
            // Chạy về phía player
            Vector2 chaseDir = (player.position - transform.position).normalized;
            moveDir = chaseDir;
            rb.MovePosition(rb.position + chaseDir * runSpeed * Time.fixedDeltaTime);
        }
        else if (state == State.Idle || state == State.Attack)
        {
            rb.velocity = Vector2.zero;
        }

        // Đảo chiều sprite dựa trên hướng di chuyển
        if (moveDir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Coroutine tuần tra: khi trong trạng thái Walk, chọn hướng ngẫu nhiên mỗi roamInterval giây
    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            if (state == State.Walk)
            {
                moveDir = GetRandomDirection();
                yield return new WaitForSeconds(roamInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    // Sinh ra hướng ngẫu nhiên (vector đơn vị)
    private Vector2 GetRandomDirection()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        return new Vector2(randX, randY).normalized;
    }

    // Hàm tấn công: kích hoạt Trigger trong Animator và gây sát thương cho player
    private void AttackPlayer()
    {
        animator.SetTrigger("AttackTrigger");
        if (player != null)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(attackDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("AttackTrigger");
            PlayerHealth playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}


