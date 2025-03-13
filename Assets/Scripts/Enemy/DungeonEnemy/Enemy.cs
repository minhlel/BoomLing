using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] protected int enemyMoveSpeed = 1;
   [SerializeField] protected int maxEnemyHp = 100;
   [SerializeField] private float moveDistance = 10f;
   protected int currentEnemyHp;
   [SerializeField] protected int enterDamage = 3;
   [SerializeField] protected int stayDamage = 1;
   protected PlayerController player;
   protected PlayerHealth playerHealth;
   // Variables for random movement
    private Vector2 randomTarget;
    private float changeDirectionTimer;
    [SerializeField] private float changeDirectionInterval = 2f; // How often to pick new direction
    [SerializeField] protected EnemyAudio enemyAudio;
   protected virtual void Start()
   {
        player = FindAnyObjectByType<PlayerController>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        currentEnemyHp = maxEnemyHp;
        PickNewRandomTarget(); // Set initial random target
   }
   protected virtual void Update()
   {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= moveDistance)
            {
                MoveToPlayer();
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            Patrol(); // If no player exists, keep patrolling
        }
   }
   protected void MoveToPlayer()
   {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
        FlipEnemy();
   }
   protected void Patrol()
    {
        // Move towards random target
        transform.position = Vector2.MoveTowards(transform.position, randomTarget, enemyMoveSpeed * Time.deltaTime);

        // Update timer and pick new target when time's up
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            PickNewRandomTarget();
            changeDirectionTimer = changeDirectionInterval;
        }

        // Optional: Flip based on movement direction
        FlipEnemyBasedOnMovement();
     //     Debug.LogError("Patrol");

    }
    protected void PickNewRandomTarget()
    {
        // Generate random point within a radius around current position
        float patrolRadius = 5f; // Adjust this value to control patrol range
        randomTarget = (Vector2)transform.position + Random.insideUnitCircle * patrolRadius;
    }
   protected void FlipEnemy()
   {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1,1,1); 
        }
   }
   protected void FlipEnemyBasedOnMovement()
    {
        // Flip based on movement direction
        Vector2 movementDirection = randomTarget - (Vector2)transform.position;
        if (movementDirection.x != 0)
        {
            transform.localScale = new Vector3(movementDirection.x < 0 ? -1 : 1, 1, 1);
        }
    }
   public virtual void TakeDamage(int damageAmount)
   {
        currentEnemyHp -= damageAmount;
        currentEnemyHp = Mathf.Max(currentEnemyHp, 0);
        if(currentEnemyHp <= 0) 
        {
          Die();
        }
   }
   protected virtual void Die()
   {
        enemyAudio.PlayEnemyDeathSound();
        Destroy(gameObject);

   }
}