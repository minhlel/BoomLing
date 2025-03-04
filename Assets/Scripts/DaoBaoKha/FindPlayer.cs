using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public float speed = 2f; // Speed of the character
    public float detectionRange = 5f; // Range to detect the player
    private bool isAttacking = false; // Flag to check if attacking

    public Transform player;
    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        // Walking logic
        WalkAround();

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void WalkAround()
    {
        // Implement random walking logic here
        // For example, move in a random direction
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        transform.position += randomDirection * speed * Time.deltaTime;
    }

    private void Update()
    {
        // Check for player detection
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            LookAtPlayer();
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
        // Check if close enough to attack
        if (Vector3.Distance(transform.position, player.position) < 1f && !isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isAttacking = true;
        // Implement attack logic here
        // Reset isAttacking after attack animation or logic
    }
}
