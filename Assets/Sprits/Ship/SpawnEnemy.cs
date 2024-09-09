using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab that has the EnemyAI component
    public int enemiesPerWave = 10000000;
    public float spawnDelay = 0.5f;  // Delay between enemy spawns
    public Transform[] spawnPoints; // Add spawn points if needed

    private List<GameObject> spawnedEnemies = new List<GameObject>();


    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        // Check if all enemies are destroyed
        if (spawnedEnemies.Count == 0)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
            
            // Call any initialization for EnemyAI here if needed
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.StartCoroutine(enemyAI.UpdatePath());
            }

            yield return new WaitForSeconds(spawnDelay);  // Delay between each spawn
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // You can randomize or select a spawn point from the list
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomIndex].position;
        }

        // Default random spawn position if no spawn points are set
        return new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
    }
}
