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
    private Vector2 vectorBool = new Vector2(0, 0);
    public int damage;
    [SerializeField] private GameObject objetive;
    public string tagFind;
    public int maxHealth = 20;
    private int currentHealth;
    public float flashDuration = 0;
    public Color originalColor = Color.white;
    private SpriteRenderer spriteColor;
    private bool detectandoJugador = false;
    private bool puedeRotar = true;
    private Coroutine disparoCoroutine;
    public float anguloVision = 45f;
    public float distanciaVision = 10f;
    public float rotacionPorCiclo = 30f;
    public float tiempoEntreRotaciones = 2f;
    public float tiempoParaReanudar = 3f;

    private void Start()
    {
        objetive = GameObject.FindWithTag("Player");
        spriteColor = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        StartCoroutine(RotarConoVision());
    }
    void Update()
    {
        DetectarJugador();
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

    IEnumerator RotarConoVision()
    {
        while (true)
        {
            if (puedeRotar)
            {
                transform.Rotate(0, 0, rotacionPorCiclo);
                yield return new WaitForSeconds(tiempoEntreRotaciones);
            }
            else
            {
                yield return null;
            }
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
        gameObject.SetActive(false); // Desactiva el jugador
    }

    private IEnumerator TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida restante: " + currentHealth);


        if (currentHealth <= 0)
        {
            Die();
        }

        spriteColor.color = Color.red; // Cambia a rojo
        yield return new WaitForSeconds(flashDuration); // Espera un momento
        spriteColor.color = originalColor; // Vuelve al color original
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("CollisionEnemy");

        if (other.CompareTag("Bullet")) // Si choca con un objeto con etiqueta "Enemy"
        {
            //TakeDamage(2);
            StartCoroutine(TakeDamage(other.GetComponent<bulletController>().damage));
        }
    }

    void DetectarJugador()
    {
        Vector3 direccionAlObjetivo = (objetive.transform.position - transform.position).normalized;
        float distanciaAlObjetivo = Vector2.Distance(transform.position, objetive.transform.position);
        float anguloAlObjetivo = Vector2.Angle(transform.up, direccionAlObjetivo);

        if (distanciaAlObjetivo <= distanciaVision && anguloAlObjetivo <= anguloVision / 2)
        {
            if (!detectandoJugador)
            {
                detectandoJugador = true;
                puedeRotar = false;
                if (disparoCoroutine == null)
                {
                    disparoCoroutine = StartCoroutine(Disparar());
                }
            }
        }
        else if (detectandoJugador)
        {
            detectandoJugador = false;
            if (disparoCoroutine != null)
            {
                StopCoroutine(disparoCoroutine);
                disparoCoroutine = null;
            }
            StartCoroutine(ReanudarRotacion());
        }
    }

    IEnumerator Disparar()
    {
        while (detectandoJugador)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator ReanudarRotacion()
    {
        yield return new WaitForSeconds(tiempoParaReanudar);
        puedeRotar = true;
    }
}
