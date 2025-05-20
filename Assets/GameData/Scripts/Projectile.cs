using UnityEngine;

// вазелиновое дрисло

public class Projectile : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy")) // бэууум
        {
           
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); 
        }
        else if (collision.CompareTag("Wall")) // огонь по блядскому хутору.
        {
            Destroy(gameObject);
        }
    }
}   
