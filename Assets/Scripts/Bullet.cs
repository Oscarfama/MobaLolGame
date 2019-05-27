using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float damage = 10f;
    public GameObject ImpactEffect;
    private bool flag;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame) // when hit target
        {
            HitTarget();
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectIns= (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);

        Destroy(effectIns, 2f);
        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform enemy) 
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
}
