using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public TopDownMovement player;
    private Vector2 memoryDirection;
    private Vector2 vectorBool = new Vector2(0, 0);
    public int damage;
    public string tagFind;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (player.GetDireccion() != vectorBool)
        {
            memoryDirection = player.GetDireccion();
        }
    }

    void Shoot()
    {

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        projectile.GetComponent<bulletController>().damage = damage;
        projectile.GetComponent<bulletController>().tagFind = tagFind;
        rb.velocity = memoryDirection * projectileSpeed; // Disparo en la dirección del firePoint


    }
}

