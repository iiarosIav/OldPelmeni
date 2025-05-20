using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

// комментов не будет, ебись сам.

public class Enemy : MonoBehaviour
{
    public int health;
    public float followSpeed;
    public Transform target;
    public float followDistance = 2f;
    public int damage = 10;
    public int scoreValue = 10;
    public GameObject _player;


    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            ScoreManager.Instance.AddScore(scoreValue);
        }

        if (target != null)
        {
            
            Vector3 direction = target.position - transform.position;

            if (direction.magnitude > followDistance)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position - direction.normalized * followDistance,
                    followSpeed * Time.deltaTime
                );
            }

            if (direction.magnitude == followDistance)
            {
                GiveDamage();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void GiveDamage() 
    {
        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"Enemy attacked! Player lost {damage} HP.");
        }
    }
}
