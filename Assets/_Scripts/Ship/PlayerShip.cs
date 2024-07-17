using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private GameObject playerShip;
    [SerializeField] private Transform holderBullets;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private GameObject playerBullet2;
    [SerializeField] private Transform rotation;
    [SerializeField] private int countBullet = 0;
    [SerializeField] private List<GameObject> bulletList = new List<GameObject>();
    [SerializeField] private HpPlayer hpPlayer;

    [SerializeField] private List<SpawnEnemy> listSpawnObject;

    private float speed = 0.2f;

    public BulletType bulletType;

    void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    void Update()
    {
        MoveShip();
        if (ScoreManager.Instance.score == 10)
        {
            bulletType = BulletType.PlayerBullet2;
        }
    }

    public GameObject FindEnemyNearest()
    {
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (SpawnEnemy spawnEnemy in listSpawnObject)
        {
            foreach (GameObject enemy in spawnEnemy.GetActiveEnemies()) // Use the getter method
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }
        }

        return nearestEnemy;
    }

    private void MoveShip()
    {
        float leftRight = Input.GetAxisRaw("Horizontal");
        float upDown = Input.GetAxisRaw("Vertical");
        if (leftRight != 0 || upDown != 0)
        {
            Vector3 newPosition = playerShip.transform.position + new Vector3(leftRight * speed, upDown * speed);
            playerShip.transform.DOMove(newPosition, 0.1f);
        }
    }

    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            float rotationAngle = playerShip.transform.rotation.eulerAngles.z;
            Vector3 direction = Quaternion.Euler(0f, 0f, rotationAngle) * Vector3.up;
            direction = new Vector3(direction.x, direction.y, 100f);
            if (bulletType == BulletType.PlayerBullet1)
            {
                HandleBulletSpawning(playerBullet, direction);
            }
            else if (bulletType == BulletType.PlayerBullet2)
            {
                HandleBulletSpawning(playerBullet2, direction);
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void HandleBulletSpawning(GameObject bulletPrefab, Vector3 direction)
    {
        if (bulletPrefab == null || holderBullets == null || playerShip == null)
        {
            return;
        }

        if (countBullet < 4)
        {
            var bullet = Instantiate(bulletPrefab, playerShip.transform.position, Quaternion.identity, holderBullets);
            var bulletComponent = bullet.GetComponent<PlayerBullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Move(direction);
            }
            else
            {
                return; // Early return if the component is missing
            }

            bulletList.Add(bullet);
            countBullet++;
        }
        else
        {
            if (bulletList.Count > 4)
            {
                Destroy(bulletList[0]);
                bulletList.RemoveAt(0);
            }

            var newBullet = Instantiate(bulletPrefab, playerShip.transform.position, Quaternion.identity, holderBullets);
            var bulletComponent = newBullet.GetComponent<PlayerBullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Move(direction);
            }
            else
            {
                return; // Early return if the component is missing
            }

            bulletList.Add(newBullet);
        }
    }


    public void SetBulletType(BulletType type)
    {
        bulletType = type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            hpPlayer.SubHealth(50);
        }
        else if (collision.CompareTag("AddHealthItem"))
        {
            hpPlayer.AddHealth(20);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("AddFullHealthItem"))
        {
            hpPlayer.ResetHealth();
            Destroy(collision.gameObject);
        }
    }
}

public enum BulletType
{
    PlayerBullet1,
    PlayerBullet2
}
