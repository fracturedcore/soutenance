using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreText;
    [SerializeField]
    private TextMeshProUGUI _highScoreGOText;
    [SerializeField]
    private TextMeshProUGUI _scoreGOText;
    public static int score;
    public static int highScore;

    void Update()
    {
        if (score <= 0)
        {
            score = 0;
        }
        
        _scoreText.text = "SCORE : " + score.ToString();
        //_highScoreText.text = "HIGH SCORE : " + highScore;
        _scoreGOText.text = "SCORE : " + score.ToString();
        _highScoreGOText.text = "HIGH SCORE : " + highScore;
    }

    public static void AddScore(int pScore)
    {
        score += pScore;
    }
    
    public static void RemoveScore(int pScore)
    {
        score -= pScore;
    }
}
