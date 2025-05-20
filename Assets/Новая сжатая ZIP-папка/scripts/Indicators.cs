using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100;

    private float timeBetweenAttack;
    private float startTimeBetweenAttack = 1f;

    
    public float secondsToEmptyHealth = 60f;

    void Start()
    {
        healthBar.fillAmount = healthAmount / 100;
    }

    void Update()
    {
        healthBar.fillAmount = healthAmount / 100;
    }

    public void ChangeHealthAmount(float changeValue)
    {
        if (healthAmount + changeValue > 100)
        {
            healthAmount = 100;
        }
        else
        {
            healthAmount += changeValue;
        }
    }

    void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "Slime")
            StartCoroutine(ToDamage());
    }

    void OnCollisionExit2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "Slime")
            StopAllCoroutines();
    }

    private IEnumerator ToDamage()
    {
        //Отнимаем 1ед здоровья пока здоровье есть или пока корутина не будет остановлена
        while (healthAmount > 0)
        {
            healthAmount -= 10;
            yield return new WaitForSeconds(1.0f);
        }
    }
}


