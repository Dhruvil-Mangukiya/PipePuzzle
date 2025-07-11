using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float levelTime = 60f;
    private bool isRunning = false;
    private StarManager starManager;
    private GameManager_script gameManager;
    public float maxTime;

    void Start()
    {
        starManager = FindObjectOfType<StarManager>();
        gameManager = FindObjectOfType<GameManager_script>();
        maxTime = levelTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isRunning)
        {
            levelTime -= Time.deltaTime;
            if (levelTime <= 0)
            {
                levelTime = 0;
                StopTimer();
                gameManager.GameOver();
            }
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = levelTime.ToString("F2"); 
    }

    public void StopTimer()
    {
        isRunning = false;
        starManager.CalculateStars(levelTime, maxTime);
    }

    public bool IsTimerRunning()
    {
        return isRunning;
    }

    public void StratTimer()
    {
        isRunning = true;
    }
}
