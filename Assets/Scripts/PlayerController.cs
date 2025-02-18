using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    public TextMeshProUGUI textMeshPro;
    private SpriteRenderer spriteColor;
    public float flashDuration = 0;
    public Color originalColor = Color.white;

    void Start()
    {
        currentHealth = maxHealth;
        spriteColor = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        textMeshPro.text = "Vida " + currentHealth + "/20";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");

        if (other.CompareTag("RedBullet")) // Si choca con un objeto con etiqueta "Enemy"
        {
            //TakeDamage(2);
            StartCoroutine(TakeDamage(other.GetComponent<bulletController>().damage));
        }

        if (other.CompareTag("Enemy"))
        {
            //TakeDamage(2);
            StartCoroutine(TakeDamage(2));
        }
    }

    /*void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida restante: " + currentHealth);
        spriteColor.color = Color.red;
        spriteColor.color = Color.white;


        if (currentHealth <= 0)
        {
            Die();
        }
    }*/

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

}