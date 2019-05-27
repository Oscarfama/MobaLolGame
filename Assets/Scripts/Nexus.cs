using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    public void GetHit(float amount)
    {
       health -= amount;
        Debug.Log(health);
        healthBar.fillAmount = health / startHealth;
       if (health <= 0)
       {
           Destroy(gameObject);
       }
    }
}
