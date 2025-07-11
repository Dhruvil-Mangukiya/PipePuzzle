using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void GotoMainmenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScneIndex = currentSceneIndex + 1;

        if (nextScneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LevelMenu.currLevel = nextScneIndex;

            UnlockNewLevel(nextScneIndex);
            SceneManager.LoadScene(nextScneIndex);
        }
    }

    void UnlockNewLevel(int indexToUnlock)
    {
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex", 1);

        if (indexToUnlock > reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", indexToUnlock);
        }

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (indexToUnlock > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", indexToUnlock);
        }
        PlayerPrefs.Save();
    }
}
