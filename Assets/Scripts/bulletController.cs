using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public string tagFind;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    void Update()
    {

        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BulletCoallision");
        if (!other.CompareTag(tagFind) && !other.CompareTag("RedBullet")) // Evitar destruir si colisiona con el Player
        {
            Destroy(gameObject);
        }
    }

    int GetDamage()
    {
        return damage;
    }

}
