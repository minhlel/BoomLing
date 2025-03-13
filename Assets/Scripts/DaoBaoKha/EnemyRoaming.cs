using System.Collections;
using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    public enum State { Idle, Walk, Run, Attack }
    private State state = State.Walk; // Mặc định enemy tuần tra nếu chưa có player

    [Header("Cài đặt Tuần tra")]
    [SerializeField] private float roamInterval = 2f;
    [SerializeField] private float walkSpeed = 2f;

    [Header("Cài đặt Đuổi theo")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float runSpeed = 4f;

    [Header("Cài đặt Tấn công")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private int attackDamage = 10;

    [Header("Tham chiếu")]
    // Enemy sẽ tự tìm đối tượng có tag "Player"
    private Transform player;
    public Animator animator;  // Animator cần có parameter: IsWalking, IsRunning, IsAttacking

    private Rigidbody2D rb;
    private Vector2 moveDir;

    // Quản lý cooldown tấn công
    private bool canAttack = true;
    private float attackTimer = 0f;

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
        // Tìm player theo tag "Player" nếu chưa có
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // Chuyển đổi trạng thái dựa trên khoảng cách đến player
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist <= detectionRange)
            {
                if (dist > attackRange)
                {
                    state = State.Run;
                }
                else
                {
                    state = State.Attack;
                }
            }
            else
            {
                state = State.Walk;
            }
        }
        else
        {
            state = State.Walk;
        }

        // Giảm cooldown tấn công
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                canAttack = true;
            }
        }

        // Cập nhật các parameter cho Animator dựa trên trạng thái
        switch (state)
        {
            case State.Idle:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", false);
                break;
            case State.Walk:
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", false);
                break;
            case State.Run:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsAttacking", false);
                break;
            case State.Attack:
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (state == State.Walk)
        {
            rb.MovePosition(rb.position + moveDir * walkSpeed * Time.fixedDeltaTime);
        }
        else if (state == State.Run && player != null)
        {
            Vector2 chaseDir = (player.position - transform.position).normalized;
            moveDir = chaseDir;
            rb.MovePosition(rb.position + chaseDir * runSpeed * Time.fixedDeltaTime);
        }
        else // Idle hoặc Attack
        {
            rb.velocity = Vector2.zero;
        }

        // Đảo chiều sprite dựa trên hướng di chuyển (flip theo trục X)
        if (moveDir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

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

    private Vector2 GetRandomDirection()
    {
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        return new Vector2(randX, randY).normalized;
    }

    // Sử dụng OnTriggerStay2D để gây sát thương liên tục khi player vẫn ở trong vùng collider
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && state == State.Attack && canAttack)
        {
            AttackPlayer();
        }
    }

    // Hàm tấn công: gây sát thương cho player
    private void AttackPlayer()
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(attackDamage);
            Debug.Log("Enemy tấn công, gây " + attackDamage + " sát thương!");
        }
        canAttack = false;
        attackTimer = attackCooldown;
    }
}
