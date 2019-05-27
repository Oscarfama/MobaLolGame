using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public int index;
    private Transform target;
    public string enemyTag = "EnemyTurret";
    public float damage = 5;
    public string nexusString;

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
        StartCoroutine(Think());
    }

    IEnumerator Think()
    {
        while (true)
        {
            GameObject[] enemyTurrets = GameObject.FindGameObjectsWithTag(enemyTag);
            target = WayPoints.points[index];
            /*
             if (enemyTurret.gameObject.GetComponent<Turret>())
            {
                Debug.Log(enemyTurret.gameObject.componen);
            }
            */
            if (shouldMove)
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

                if (Vector3.Distance(transform.position, target.position) <= 0.2f)
                {
                    Die();
                }
            }
            foreach (var enemyTurret in enemyTurrets)
            {
                if (enemyTurret != null)
                {
                    Turret turret = (Turret)enemyTurret.GetComponent(typeof(Turret));
                    if (turret != null)
                    {
                        float dist = Vector3.Distance(enemyTurret.transform.position, transform.position); //////asdfasdfasdfa
                        if (dist < range && dist > 8f)
                        {
                            target = enemyTurret.transform;
                            Vector3 dir = target.position - transform.position;
                            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
                        }
                        else if (dist < 8f)
                        {
                            target = enemyTurret.transform;
                            GotoNextTarget(target, 1, false);
                            yield return new WaitForSeconds(1.25f);
                            turret.GetHit(damage);
                        }else
                        {
                            GotoNextTarget(target, 0,true);
                        }
                    }
                }
                else
                {
                    GotoNextTarget(target, 0, true);
                }
            }
            if (enemyTurrets.Length == 0)
            {
                target = WayPoints.points[0];
                GotoNextTarget(target, 0,true);
            }
            yield return new WaitForSeconds(0f);
        }
    }
    
    private void GotoNextTarget(Transform target, int AnimationNumber, bool flag)
    {
        animator.SetInteger("ToDo", AnimationNumber);
        shouldMove = flag;
        transform.LookAt(target);
    }

    public bool TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == nexusString)
        {
            Nexus nexus = (Nexus)collision.gameObject.GetComponent(typeof(Nexus));
            nexus.GetHit(damage *2);
            Die();
        }
    }
}
