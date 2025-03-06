using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // Prefab của đạn
    [SerializeField] private Transform firePoint; // Vị trí bắn
    [SerializeField] private float bulletSpeed = 10f; // Tốc độ đạn
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private SpriteRenderer mySpriteRender;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //MouseFollowWithOffset();
        HandleShooting();
    }
       private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - playerController.transform.position; // Hướng từ player đến chuột

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerController.transform.position.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            //mySpriteRender.flipY = true;
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            //mySpriteRender.flipY = false;
        }
    }
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Chuột trái
        {
            Shoot();
        }
    }

    // private void Shoot()
    // {
    //     GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //     Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    //     if (bulletRb != null)
    //     {
    //         bulletRb.velocity = firePoint.right * bulletSpeed;
    //     }

    //     // Huỷ viên đạn sau 2 giây để tránh rác bộ nhớ
    //     Destroy(bullet, 2f);
    // }

private void Shoot()
{
    // Tính vị trí chuột trong không gian thế giới
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePos.z = 0; // Đặt z = 0 vì game 2D chỉ cần trục x, y
    AudioManager.Instance.PlaySFX(AudioManager.Instance.gunPlayer);
    // Tính hướng từ firePoint đến vị trí chuột
    Vector3 direction = (mousePos - firePoint.position).normalized;

    // Kiểm tra nếu chuột ở bên trái người chơi
    Vector3 spawnPosition = firePoint.position;
    if (mousePos.x < playerController.transform.position.x)
    {
        // Đổi vị trí x của firePoint sang đối xứng
        spawnPosition.x = playerController.transform.position.x - (firePoint.position.x - playerController.transform.position.x);
    }

    // Tạo viên đạn từ vị trí đã điều chỉnh
    GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

    // Lấy Rigidbody2D của viên đạn và gắn vận tốc
    Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
    if (bulletRb != null)
    {
        bulletRb.velocity = direction * bulletSpeed;
    }

    // Huỷ viên đạn sau 2 giây để tránh rác bộ nhớ
    Destroy(bullet, 2f);
}


}
