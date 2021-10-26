using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 100;
    public static int Lives;
    public int startLives = 3;

    public static int HighScore;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
    }
}
