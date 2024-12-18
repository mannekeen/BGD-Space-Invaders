using UnityEngine;
using TMPro; // For TextMeshPro (use UnityEngine.UI if using standard Text)

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the ScoreText
    private int score = 0;           // Initial score

    public void AddScore(int value)
    {
        score += value;              // Increment the score
        scoreText.text = "Score: " + score; // Update the UI
    }
}
