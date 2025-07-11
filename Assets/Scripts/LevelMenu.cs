using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public Sprite emptyStar;
    public Sprite filledStar;

    public static int currLevel;
    public static int unlockedLevel;

    void Awake()
    {
        ButtonsToArray();
    }

    void OnEnable()
    {
        RefreshLevelButtons();
    }

    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    public void RefreshLevelButtons()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;

            int stars = PlayerPrefs.GetInt("Level_" + (i + 1) + "_Stars", 0);
            UpdateButtonStars(buttons[i], stars);
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    private void UpdateButtonStars(Button button, int stars)
    {
        Debug.Log(button.name + " has " + stars + " stars");

        Transform starContainer = button.transform.Find("Stars");
        if (starContainer != null)
        {
            for (int j = 0; j < starContainer.childCount; j++)
            {
                Image starImage = starContainer.GetChild(j).GetComponent<Image>();
                if (starImage != null)
                {
                    starImage.sprite = (j < stars) ? filledStar : emptyStar;
                }
            }
        }
    }

    public void OpenLevel(int levelId)
    {
        currLevel = levelId;
        string levelName = "Level " + currLevel;
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f;
    }

}
