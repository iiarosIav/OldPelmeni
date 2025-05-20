using UnityEngine;

// хилка. Просто хилка, не более.

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healAmount = 25; 
    [SerializeField] private KeyCode pickupKey = KeyCode.F; 
    [SerializeField] private float pickupRadius = 1.5f; 

    private bool _isPlayerNear;
    private GameObject _player;

    private void Update()
    {
        if (_isPlayerNear && Input.GetKeyDown(pickupKey))
        {
            TryHealPlayer();
        }
    }

    private void TryHealPlayer()
    {
        if (_player != null)
        {
            var playerHealth = _player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = true;
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNear = false;
            _player = null;
        }
    }
}