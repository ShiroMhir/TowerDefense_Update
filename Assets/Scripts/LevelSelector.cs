using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour

{
    public Button[] levelsButtons;
    public SceneFader fader;

    private void Start()
    {
        int levelCompleted = PlayerPrefs.GetInt("levelCompleted", 1);

        for (int i = 0; i < levelsButtons.Length; i++)
        {
            if(i+1 > levelCompleted)
            {
                levelsButtons[i].interactable = false;
            }
            
        }
    }
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
