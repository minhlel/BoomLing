using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;



    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void OnDestroy()
    {
        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    { 
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        bool isShooting = Input.GetMouseButton(0); // Nhấn chuột trái để bắn
        myAnimator.SetBool("shoot", isShooting ? true : false); 
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    // private void AdjustPlayerFacingDirection() {
    //     Vector3 mousePos = Input.mousePosition;
    //     Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

    //     if (mousePos.x < playerScreenPoint.x) {
    //         mySpriteRender.flipX = true;
    //     } else {
    //         mySpriteRender.flipX = false;
    //     }
    // }

    public bool IsFacingLeft { get; private set; } // Biến công khai để kiểm tra hướng nhân vật

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            IsFacingLeft = true; // Cập nhật hướng
        }
        else
        {
            mySpriteRender.flipX = false;
            IsFacingLeft = false; // Cập nhật hướng
        }
    }

}
