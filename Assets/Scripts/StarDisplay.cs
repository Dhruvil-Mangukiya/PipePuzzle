    using UnityEngine;
    using UnityEngine.UI;
    
    public class StarDisplay : MonoBehaviour
    {
        public int levelIndex;
        public Image[] starImages;
    
        void Start()
        {
            UpdateStarDisplay();
        }
    
        public void UpdateStarDisplay()
        {
            int stars = GetStarsForLevel(levelIndex);
    
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].enabled = i < stars;
            }
        }
    
        private int GetStarsForLevel(int levelIndex)
        {
            return PlayerPrefs.GetInt("Level" + levelIndex + "Stars", 0);
        }
    }
