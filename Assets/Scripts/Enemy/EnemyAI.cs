using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing,
        Attacking
    }

    [Header("Chase Settings")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float roamingInterval = 2f; // Thời gian giữa các lần roaming
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damageAmount = 10;

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Transform playerTransform;
    private float roamingTimer;
    private Animator myAnimator;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
        state = State.Roaming;
        roamingTimer = roamingInterval;
    }

    private void Update()
    {
        if (PlayerHealth.Instance != null && !PlayerHealth.Instance.CheckIfPlayerDeath())
        {
            switch (state)
            {
                case State.Roaming:
                    myAnimator.SetBool("Attack", false);
                    HandleRoaming();
                    break;

                case State.Chasing:
                    myAnimator.SetBool("Attack", false);
                    ChasePlayer();
                    break;

                case State.Attacking:
                    myAnimator.SetBool("Attack", true);
                    AttackPlayer();
                    break;
            }
        }
    }

    private void HandleRoaming()
    {
        // Giảm thời gian đếm ngược
        roamingTimer -= Time.deltaTime;

        if (roamingTimer <= 0f)
        {
            // Tạo vị trí mới và di chuyển đến đó
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathfinding.MoveTo(roamPosition);
            // Reset bộ đếm thời gian
            roamingTimer = roamingInterval;
            // Kiểm tra xem Player có ở gần không
            DetectPlayer();
        }
    }

    private Vector2 GetRoamingPosition()
    {
        // Random vị trí xung quanh
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange)
        {
            state = State.Chasing; // Chuyển sang trạng thái đuổi theo
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            state = State.Attacking;
        }
        else if (distanceToPlayer > detectionRange)
        {
            state = State.Roaming;
        }
        else
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            enemyPathfinding.moveSpeed = chaseSpeed;
            enemyPathfinding.MoveTo(direction);
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void AttackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > attackRange)
        {
            state = State.Chasing;
        }
        else
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}

