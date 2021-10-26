using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        scoreText.text = "0";
        int score = 0;

        yield return new WaitForSeconds(0.7f);

        while (score < PlayerStats.HighScore)
        {
            score += 5;
            scoreText.text = score.ToString();

            yield return new WaitForSeconds(0.01f);
        }
    }
}
