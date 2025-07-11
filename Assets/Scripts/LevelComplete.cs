using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public static void OnLevelComplete(int level, int stars)
    {
        int previousStars = PlayerPrefs.GetInt("Level_" + level + "_Stars", 0);

        if (stars > previousStars)
        {
            PlayerPrefs.SetInt("Level_" + level + "_Stars", stars);
        }

        if (LevelMenu.currLevel == LevelMenu.unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", LevelMenu.unlockedLevel + 1);
        }

        PlayerPrefs.Save();
    }
}
