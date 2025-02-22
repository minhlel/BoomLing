using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //[SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameObject bulletPrefab; // Prefab của đạn
    [SerializeField] private Transform firePoint; // Vị trí bắn
    [SerializeField] private float bulletSpeed = 10f; // Tốc độ đạn
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollowWithOffset();
         HandleShooting();
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

 private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Chuột trái
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Tạo viên đạn từ prefab tại firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Lấy Rigidbody2D của viên đạn và gắn vận tốc
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.right * bulletSpeed;
        }

        // Huỷ viên đạn sau 2 giây để tránh rác bộ nhớ
        Destroy(bullet, 2f);
    }



}