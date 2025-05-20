using UnityEngine;
using UnityEngine.UI;


public class healthBar : MonoBehaviour
{
    public Image hpBarTwo;
    public healthInfo player;

    void Start()
    {
        hpBarTwo = GetComponent<Image>();
        player = FindObjectOfType<healthInfo>();
    }


    void Update()
    {
        hpBarTwo.fillAmount = player.HP / player.maxHP;
    }
}
