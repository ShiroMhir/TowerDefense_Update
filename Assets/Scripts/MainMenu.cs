using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelSelecSceneName = "LevelSelect";
    public string optionsSceneName = "LevelSelect";
    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelSelecSceneName);
    }
    public void Quit()
    {
        Debug.Log("Exiting game...");
        Application.Quit();         
    }

    public void Options()
    {
        sceneFader.FadeTo(optionsSceneName);
    }
}
