using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class TorretEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private Vector2 vectorBool = new Vector2(0, 0);
    public int damage;
    [SerializeField] private GameObject objetive;
    public string tagFind;

    private void Start()
    {
        objetive = GameObject.FindWithTag("Player");
        Shoot();
    }
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Vector3 memoryDirection = (objetive.transform.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        projectile.GetComponent<bulletController>().damage = damage;
        projectile.GetComponent<bulletController>().tagFind = tagFind;
        rb.velocity = memoryDirection * projectileSpeed; // Disparo en la dirección del firePoint


    }
}
