using System.Collections;
using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    [Header("Roaming Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float roamInterval = 2f;

    private enum State
    {
        Roaming
    }

    private State state;
    private Rigidbody2D rb;
    private Vector2 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

    // Flip sprite khi di chuyển trái/phải
    if (moveDir.x > 0)
        transform.localScale = new Vector3(1, 1, 1); // Quay về phải
    else if (moveDir.x < 0)
        transform.localScale = new Vector3(-1, 1, 1); // Quay về trái
    }

    // Thay thế cho MoveTo() của EnemyPathfinding
    private void SetMoveDirection(Vector2 direction)
    {
        moveDir = direction;
    }

    // Thay thế cho RoamingRoutine() và AI logic
    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            // Chọn hướng ngẫu nhiên
            Vector2 roamDir = GetRandomDirection();
            // Gán cho moveDir
            SetMoveDirection(roamDir);

            // Chờ roamInterval giây rồi đổi hướng
            yield return new WaitForSeconds(roamInterval);
        }
    }

    private Vector2 GetRandomDirection()
    {
        // Random trong khoảng -1 -> 1, rồi normalized
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
