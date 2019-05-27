using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 4f;
    private float countdown = 2f;

    private int waveIndex =2;

    public Text text;


    private void Start()
    {
        StartCoroutine(textTimer());
    }
    void Update()
    {

        if (countdown <= 0f)
        {
             StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves * 10;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator textTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            text.gameObject.SetActive(false);
        }

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
