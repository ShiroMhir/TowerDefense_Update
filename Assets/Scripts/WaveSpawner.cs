using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countDown = 10f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    public GameManager gameManager;

    void Update()
    {
        if (enemiesAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.LevelComplete();

            this.enabled = false;
        }


        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }
        
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        countDown -= Time.deltaTime;
               
        waveCountdownText.text = string.Format("{0:00.0}", countDown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.HighScore+= 100;
        
        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;

        

        for (int i = 0; i < wave.count; i++)
        {
            //Debug.Log('Enemy_incoming!');
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f /wave.Rate);
        }

        waveIndex++;
        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
