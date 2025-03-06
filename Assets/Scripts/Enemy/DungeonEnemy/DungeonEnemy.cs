using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DungeonEnemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1f; // Speed of the enemy
    [SerializeField] protected float detectionRange = 5f; // Distance at which the enemy starts moving toward the player
    protected PlayerController player; // Reference to the player 
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>(); // Find the player object in the scene
    }
    protected virtual void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            MoveToPlayer(); // Call the function to move the enemy towards the player
        }
    }
    protected void MoveToPlayer()
    {
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            FlipEnemy();
        }
    }
    protected void FlipEnemy()
    {
        if(player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1 : 1, 1, 1); 
        }
    }
}