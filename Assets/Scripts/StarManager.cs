using UnityEngine;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public GameObject[] stars;
    private int starCount;

    public int CalculateStars(float remainingTime, float maxTime)
    {
        float percentage = (remainingTime / maxTime) * 100; 

        if (percentage >= 50)
            starCount = 3;
        else if (percentage >= 30)
            starCount = 2;  
        else if (percentage >= 15)
            starCount = 1;
        else
            starCount = 0;

        ShowStars(starCount);
        return starCount;
    }

    private void ShowStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < count); 
        }
    }
}
