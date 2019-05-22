using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 4f;
    private float countdown = 2f;

    private int waveIndex =1;

     void Update()
    {

        if (countdown <= 0f)
        {
             StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves * 2 ;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        // waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.rotation);
    }

    
}
