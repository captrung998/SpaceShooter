using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using DG.Tweening;

public class EnemyAI : MonoBehaviour
{
    public Seeker seeker;
    private Transform target;
    public float moveSpeed;
    public float nextWPDistance;
    public float updatePathInterval = 0.5f;  // Increased interval to reduce computation load
    public SpriteRenderer characterSR;
    private Path path;

    private Coroutine moveCoroutine;
    private SpawnEnemy spawner;

    public float separationRadius = 1.0f;
    public float separationForce = 1.0f;

    private void Start()
    {
        spawner = FindObjectOfType<SpawnEnemy>();

        var shipMovement = FindAnyObjectByType<ShipMovement>();
        if (shipMovement != null)
        {
            target = shipMovement.transform;
        }
        else
        {
            Debug.LogError("No ShipMovement object found in the scene!");
            return;
        }

        StartCoroutine(UpdatePath());
    }

    public IEnumerator UpdatePath()
    {
        while (target != null)  // Continue updating path as long as target exists
        {
            if (seeker != null && seeker.IsDone())
            {
                seeker.StartPath(transform.position, target.position, OnPathCallback);
            }
            else
            {
                Debug.LogError("Seeker component is not assigned, busy, or target is null.");
            }

            yield return new WaitForSeconds(updatePathInterval);  // Wait before recalculating path
        }
    }

    void OnPathCallback(Path p)
    {
        if (p == null)
        {
            Debug.LogError("Path is null!");
            return;
        }

        path = p;

        MoveToTarget();  // After recalculating the path, move towards the target
    }

    public void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    public IEnumerator MoveToTargetCoroutine()
    {
        if (path == null || path.vectorPath == null)
        {
            Debug.LogError("Path is not initialized or vectorPath is null.");
            yield break;
        }

        int currentWp = 0;
        while (currentWp < path.vectorPath.Count)
        {
            if (ManagerController.Instance.currenScore % 10 == 0 && ManagerController.Instance.currenScore != 0)
            {
                moveSpeed += 0.05f;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWp] - (Vector2)transform.position);
            Vector3 force = direction.normalized * moveSpeed * Time.deltaTime;

            ApplySeparation();


            transform.DOMove(transform.position + force, 0).SetEase(Ease.Linear);

            // Rotate enemy towards target
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));  // Correct angle for 2D rotation
            }

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWp]);
            if (distance < nextWPDistance)
            {
                currentWp++;
            }

            yield return null;  // Wait for the next frame
        }
    }
    void ApplySeparation()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, separationRadius);

        foreach (var collider in nearbyEnemies)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector2 directionAway = (transform.position - collider.transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + directionAway, separationForce * Time.deltaTime);
            }
        }
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed(gameObject);  // Notify spawner that the enemy was destroyed
        }
    }
}
