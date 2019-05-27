using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Turret : NetworkBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public float range = 15f;
    public string enemyTag = "Enemy";

    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Health")]
    private float health;
    public float startHealth = 4;
    public Image healthBar;

    void Start()
    {
        health = startHealth;
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        float distanceToEnemy;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
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

    void Update()
    {
        if(target == null)
        {
            return;
        }
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    public void GetHit(float amount)
    {
        if (this != null) {
            health -= amount;
            healthBar.fillAmount = health / startHealth;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
