using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    public float speed = 5f;
    private CharacterController controller;
    private Vector3 moveDirection;
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        textMeshPro.text = "Vida " + currentHealth + "/20";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedBullet")) // Si choca con un objeto con etiqueta "Enemy"
        {
            TakeDamage(2);
        }

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(2);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El jugador ha muerto.");
        gameObject.SetActive(false); // Desactiva el jugador
    }
}