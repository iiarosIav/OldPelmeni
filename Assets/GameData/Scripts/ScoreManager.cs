using TMPro;
using UnityEngine;
using UnityEngine.UI;


// недоделанное говно и не реализованно, ебись сам.

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 

    public int score = 0;
    public TextMeshPro scoreText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}