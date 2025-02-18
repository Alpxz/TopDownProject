using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float detectionRadius = 10f;
    public float avoidanceForce = 5f;
    public float obstacleDetectionDistance = 1f;
    public LayerMask obstacleMask;
    public Transform playerTransform;
    public int maxHealth = 20;
    private int currentHealth;
    public float flashDuration = 0;
    public Color originalColor = Color.white;
    private SpriteRenderer spriteColor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteColor = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

    }
    void Update()
    {
        if (player == null)
            return;

        Vector2 directionToTarget = (playerTransform.position - transform.position).normalized;
        Vector2 avoidanceVector = Vector2.zero;

        // Detectar obstáculos en múltiples direcciones
        avoidanceVector += AvoidObstacle(Vector2.left);
        avoidanceVector += AvoidObstacle(Vector2.right);
        avoidanceVector += AvoidObstacle(Vector2.up);
        avoidanceVector += AvoidObstacle(Vector2.down);

        // Movimiento final combinando la persecución y la evitación
        Vector2 finalDirection = (directionToTarget + avoidanceVector).normalized;
        transform.position += (Vector3)(finalDirection * speed * Time.deltaTime);
    }

    private Vector2 AvoidObstacle(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionDistance, obstacleMask);
        if (hit.collider != null)
        {
            return -direction * avoidanceForce; // Se mueve en dirección contraria al obstáculo
        }
        return Vector2.zero;
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
}