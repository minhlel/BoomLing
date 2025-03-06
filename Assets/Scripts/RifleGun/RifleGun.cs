using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGun : MonoBehaviour
{
    private float rotateOffset = 180f; // Offset to adjust the gun's rotation angle
    [SerializeField] private Transform firePos; // Reference to the fire position of the gun
    [SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
    [SerializeField] private float shotDelay = 0.15f; // Speed of the bullet
    private float nextShot; // Timer to keep track of the time between shots
    [SerializeField] private int maxAmmo = 30; // Maximum ammo capacity
    private int currentAmmo; // Current ammo count

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo; // Set the current ammo to the maximum ammo capacity
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun(); // Call the function to rotate the gun based on the mouse position
        Shoot(); // Call the function to shoot the bullet
        Reload();
    }

    // Function to rotate the gun towards the mouse position
    void RotateGun()
    {
        // Prevents rotation calculation if the mouse is outside the screen bounds
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || 
            Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

        // Calculate the displacement vector between the gun's position and the mouse position
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convert the displacement vector into an angle in degrees
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;

        // Apply the rotation to the gun with the offset
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        if(angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }
    void Shoot()
    {
        if(Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time>nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            currentAmmo--;
        }
    }

    void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }
}
