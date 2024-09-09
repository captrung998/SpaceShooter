using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnEnemy : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform player;
    public int spawnCount = 10;
    public float minDistanceFromPlayer = 50f;


    void Start()
    {
        StartCoroutine(SpawnBase());
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        int randomValueX;
        int randomValueY;

        do
        {
            // Randomize direction by selecting either -1 or 1
            randomValueX = Random.Range(0, 2) == 0 ? -1 : 1;
            randomValueY = Random.Range(0, 2) == 0 ? -1 : 1;

            randomPosition = new Vector3(
                randomValueX * Random.Range(10f, 20f),
                randomValueY * Random.Range(10f, 20f),
                0f
            );
        } while (Vector3.Distance(randomPosition, player.position) < minDistanceFromPlayer);

        return randomPosition;
    }

    private IEnumerator SpawnBase()
    {
        while (true)
        {
            int count = 0;
            if (ManagerController.Instance.countTime % 10 == 0)
            {               
                if (count <= spawnCount)
                {
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                    count++;
                }
                else
                    count--;

            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

}
