using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject levelCompleteUI;

    public static bool gameIsOver;

   

    void Start()
    {
        gameIsOver = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
            return;
        
        // Manual ending game
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
        Toggle();
    }

    public void Toggle()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);

        if (gameOverUI.activeSelf)
        {
            // Pause game
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void LevelComplete()
    {
        gameIsOver = true;
        levelCompleteUI.SetActive(true);

    }
}
