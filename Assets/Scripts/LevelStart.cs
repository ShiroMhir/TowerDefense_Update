using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStart : MonoBehaviour
{
    public Text levelTextNumber;
    public string levelNumber = "01";

    void Awake()
    {
        levelTextNumber.text = levelNumber;
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
