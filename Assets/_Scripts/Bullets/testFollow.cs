using UnityEngine;

public class testFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float stopDistance = 0.1f;
    public float rotateSpeed = 200f;

    private bool isActive;
    private void OnEnable()
    {
        isActive = true;
    }
    private void OnDisable()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            float distance = Vector3.Distance(transform.position, target.position);

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
                transform.position = target.position;
                isActive = false;
            }
        }
    }
}
