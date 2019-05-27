using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : NetworkBehaviour
{
    PlayerMotor motor;
    public LayerMask movementMask;
    Camera cam;
    public Animator animator;
    public Image ExperienceBar;

    public float range = 12f;

    private bool shouldMove = true;
    public float damage = 10;

    private float points;

    void Start()
    {
        cam = Camera.main;
        range = 12f;
        motor = GetComponent<PlayerMotor>();
        points = 0;
        StartCoroutine(Think());
    }

    private void Update()
    {
      
    }

    IEnumerator Think()
    {
        if (!isLocalPlayer)
        {
            yield break;
        }
        while (true)
        {
            RaycastHit hit;
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, movementMask))
                {
                    float dist = Vector3.Distance(hit.transform.position, transform.position);
                    if (hit.collider.name == "Terrain")
                    {
                        SelectedTerrain(hit);
                    }
                    if (hit.transform.gameObject.tag == "EnemyTurret")
                    {
                        motor.MoveToPoint(hit.point);
                    }
                    if (hit.transform.gameObject.tag == "EnemyTurret" && dist <= range)
                    {
                        Attack(hit.transform);
                        yield return new WaitForSeconds(1.1f);
                        Turret turret = (Turret)hit.transform.gameObject.GetComponent(typeof(Turret));
                        turret.GetHit(damage);
                    }
                    if ((hit.transform.gameObject.tag == "Enemy" || hit.transform.gameObject.tag == "EnemyRed") && dist <= range)
                    {
                        Attack(hit.transform);
                        yield return new WaitForSeconds(1.1f);
                        Enemy minion = (Enemy)hit.transform.gameObject.GetComponent(typeof(Enemy));
                        bool killed = minion.TakeDamage(damage);
                        if (killed)
                        {
                            MinionKilled(10);
                        }

                    }
                }
            }
            if (motor.agent.remainingDistance <= 0)
            {
                animator.SetInteger("toDo", 1);
            }
            else
            {
                animator.SetInteger("toDo", 9);
            }
            yield return new WaitForSeconds(0f);
        }
    }

    void Attack(Transform hit)
    {
        transform.LookAt(hit.transform);
        motor.MoveToPoint(transform.position);
        animator.SetInteger("toDo", 4);
    }
    private void MinionKilled(float amount)
    {
        points += amount;
        ExperienceBar.fillAmount =points /10;

        if (points <= 10)
        {
            Debug.Log("Has Ganado");
            //Die();
        }
    }

    void SelectedTerrain(RaycastHit hit)
    {
        motor.MoveToPoint(hit.point);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
