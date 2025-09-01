using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance;
    private int Score { get; set; }

    private void Awake()
    {
        if (Instance == null && Instance != this) Destroy(gameObject);

        Instance = this;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
}
