using UnityEngine;

// да да, на изменение хп тоже отдельный код. Мне легче их подключить друг к другу, чем писать в одном файле. Голова от такого болит

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int _currentHealth;
    

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        Debug.Log($"Healed! Current health: {_currentHealth}");
        
    }

    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // тут ничо не сделано, сам можешь потом подкоректировать.
        Debug.Log("Bruh. ");

    }
}