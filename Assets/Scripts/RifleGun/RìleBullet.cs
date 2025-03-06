using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RìleBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f; // Speed of the bullet
    [SerializeField] private float timeDestroy = 0.5f; // Time before the bullet is destroyed
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy); // Destroy the bullet after a certain amount of time 
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }

    void moveBullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
