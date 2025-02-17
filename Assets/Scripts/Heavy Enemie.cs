using UnityEngine;
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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        
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
}