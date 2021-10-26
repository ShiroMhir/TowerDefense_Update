using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public GameObject bulletEffect;
    public float bulletSpeed = 50f;
    public float explosionRadius = 0f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float frameDistance = bulletSpeed * Time.deltaTime;

        if (dir.magnitude < frameDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * frameDistance, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject impactEffect = (GameObject)Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(impactEffect, 5f);

        if (explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            Damage(target);
        } 

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }     
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
