using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nexus : MonoBehaviour
{
    [Header("Health")]
    private float health;
    public float startHealth = 100;
    public Image healthBar;

    void Start()
    {
        health = startHealth;
    }

    void Update()
    {
        
    }

    public void GetHit(float amount)
    {
       health -= amount;
       healthBar.fillAmount = health / startHealth;
       if (health <= 0)
       {
           Destroy(gameObject);
       }
        Debug.Log("Nexus health " + health);
    }
}
