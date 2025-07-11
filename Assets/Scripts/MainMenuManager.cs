using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject levelMenu;

    void Start()
    {
        levelMenu.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

        Screen.SetResolution(1080, 1920, FullScreenMode.FullScreenWindow);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main_Menu")
        {
            ShowLevelMenu();
        }
    }

    public void ShowLevelMenu()
    {
        levelMenu.SetActive(true);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
