using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    // Надеюсь, поймёшь, чо тут как
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f; 
    public float fireRate = 0.2f; 
    public int maxProjectiles = 4; 



    public KeyCode shootKey = KeyCode.Space; 
    public bool autoFire = true; 



    private float nextFireTime;
    private Vector2 shootDirection = Vector2.down; 

    void Update()
    {
        HandleShootingInput();
    }

    void HandleShootingInput()
    {
        
        Vector2 newDirection = GetInputDirection();

        if (newDirection != Vector2.zero)
        {
            shootDirection = newDirection;
        }

        
        bool canShoot = (autoFire || Input.GetKey(shootKey)) && Time.time >= nextFireTime;

        if (canShoot && newDirection != Vector2.zero)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    // ты гей кстати.

    // направления для снарядов, понял да?
    Vector2 GetInputDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) direction += Vector2.up;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;

        return direction.normalized;
    }



    void Shoot() // пив пав пув бум бдыщ скдыщ нахуй
    {
        if (projectilePrefab == null) return;

        
        if (CountProjectilesOnScreen() >= maxProjectiles) return;

        
        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * projectileSpeed;
        }

       
        Destroy(projectile, 5f); 
    }

    int CountProjectilesOnScreen()
    {        
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        return projectiles.Length;
    }
}