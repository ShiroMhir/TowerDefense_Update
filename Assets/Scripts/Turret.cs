using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")] 
    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unity Setup")]
    public string enemyTag = "Enemy";

    public Transform TurnPoint;
    public float rotationSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;   
    
    


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Shortest distance of the enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        // Get direction to the enemyTag
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(TurnPoint.rotation,lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        // Rotate just y-axis
        TurnPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
