using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager_script : MonoBehaviour
{
    public static GameManager_script Instance;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;
    [SerializeField]
    int correctedPipes = 0;

    public LevelTimer levelTimer;
    public StarManager starManager;
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public Button HomeButton;
    public bool isGamePaused = false;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        WinScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes++;
        Debug.Log("correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            levelTimer.StopTimer();

            int starCount = starManager.CalculateStars(levelTimer.levelTime, levelTimer.maxTime);
            WinScreen.SetActive(true);
            HomeButton.interactable = false;
            isGamePaused = true;

            int nextLevel = LevelMenu.currLevel + 1;
            if (nextLevel > PlayerPrefs.GetInt("UnlockedLevel", 1))
            {
                PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
                PlayerPrefs.Save();
            }

            LevelComplete.OnLevelComplete(LevelMenu.currLevel, starCount);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        GameOverScreen.SetActive(true);
        HomeButton.interactable = false;
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void wrongMove()
    {
        if (correctedPipes > 0)
        {
            correctedPipes--;
        }
    }

    public void restart()
    {
        string currentScenename = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScenename);
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
