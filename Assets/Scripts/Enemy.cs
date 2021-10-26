using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    [Header("Unity Stats")]
    public Image healthBar;

    [Header("Enemy stats")]
    public GameObject deadEffect;
    public float startHealth = 100;
    private float health;
    public float speed = 10f;
    public int lootG = 15;
    private bool enemyDead = false;
    
    [Header("Enemy movement")]
    public float rotationSpeed = 10f;
    public Transform rotationBody;

    void Start()
    {
        target = Waypoints.points[0];
        health = startHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !enemyDead)
        {
            Die();
        }
    }

    void Die()
    {
        enemyDead = true;

        PlayerStats.Money += lootG;
        PlayerStats.HighScore += 10;
        WaveSpawner.enemiesAlive--;

        GameObject effect = (GameObject)Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    void Update()
    {
        // Get direction to the WayPoint
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position,target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        // Get rotation       
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(rotationBody.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        // Rotate just y-axis
        rotationBody.rotation = Quaternion.Euler(0f, rotation.y, 0f);

    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }
}
