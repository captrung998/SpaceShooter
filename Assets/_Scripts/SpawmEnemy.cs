using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 3f;

    private List<GameObject> listEnemy = new List<GameObject>();
    private Coroutine spawnCoroutine;

    void OnEnable()
    {
        // Start spawning enemies immediately when the script is enabled
        StartSpawning();
    }

    void OnDisable()
    {
        // Stop spawning enemies when the script is disabled
        StopSpawning();
    }

    void StartSpawning()
    {
        // Ensure only one coroutine is running to avoid duplicates
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemiesContinuously());
        }
    }

    void StopSpawning()
    {
        // Stop the spawning coroutine if it is running
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        while (true)
        {
            SpawnEnemyAtPosition(spawnPoint.position);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemyAtPosition(Vector3 position)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        listEnemy.Add(newEnemy); // Add the spawned enemy to the list
    }

    public List<GameObject> GetActiveEnemies()
    {
        return listEnemy;
    }
}
