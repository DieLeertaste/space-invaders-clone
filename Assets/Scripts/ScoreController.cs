using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public TMPro.TMP_Text scoreDisplay;
    private int Score { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);

        instance = this;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    private void Update()
    {
        scoreDisplay.text = "Score: " + Score;
    }
}
