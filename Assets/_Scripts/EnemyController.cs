using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerShipTaget;
    private Action<GameObject> actionEnemyDestroy;
    public float rotateSpeed = 200f; // Rotation speed of the missile
    public float searchRadius = 100f; // Adjust according to your game needs
    public float stopDistance = 0.1f;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerShipTaget == null)
        {
            playerShipTaget = GameObject.FindObjectOfType<PlayerShip>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerShipTaget != null)
        {
            Vector3 direction = (playerShipTaget.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, playerShipTaget.position);

            // Rotate towards the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Move towards the target if not yet reached
            if (distance > stopDistance)
            {
                transform.position += transform.up * speed * Time.deltaTime;
            }
            else
            {
                transform.position = playerShipTaget.position;
            }
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void InitData(Action<GameObject> actionEnemyDestroyCallback)
    {
        actionEnemyDestroy = actionEnemyDestroyCallback;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
        {
            if (actionEnemyDestroy != null)
            {
                actionEnemyDestroy.Invoke(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
