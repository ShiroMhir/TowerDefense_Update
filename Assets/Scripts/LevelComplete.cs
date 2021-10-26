using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

    [Header("NextLevel")]
    public string nextLevel = "Level02";
    public int nextLevelIndx = 2;
        

    public void Continue()
    {
        PlayerPrefs.SetInt("levelCompleted", nextLevelIndx);
        //PlayerPrefs.SetInt("playerHighSchore", PlayerStats.HighScore);

        if (nextLevelIndx < 11)
        {
            sceneFader.FadeTo(nextLevel);
        }
        else
        {
            sceneFader.FadeTo("Credits");
        }
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
