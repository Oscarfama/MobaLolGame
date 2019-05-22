using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    private Transform target;
    private int wavepointIndex = 0;
    public string enemyTag = "EnemyTurret";

    [Header("Health")]
    private float health;
    public float startHealth = 100;
    public Image healthBar;

    public int worth = 50;
    public float range = 5f;
    public Animator animator;
    private bool shouldMove = true;

    void Start()
    {
        health = startHealth;
    }

    void Update()
    {
        GameObject enemyTurret = GameObject.FindGameObjectWithTag(enemyTag);
        Turret turret = (Turret)enemyTurret.GetComponent(typeof(Turret));

        target = WayPoints.points[0];

        if (shouldMove)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                Die();
            }
        }

        if (enemyTurret != null)
        {
            float dist = Vector3.Distance(enemyTurret.transform.position, transform.position); //////asdfasdfasdfa
            if (dist < range && dist > 5f)
            {
                target = enemyTurret.transform;
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }else if(dist < 5f)
            {
                StartCoroutine(Attack(turret)); 
                shouldMove = false;
            }
        }
        else
        {
            shouldMove = true;
        }

    }
    IEnumerator Attack(Turret target)
    {
        animator.SetInteger("ToDo", 1);
        yield return new WaitForSeconds(1f);
        target.GetHit(2);

    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
