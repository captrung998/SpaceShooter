using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.CompareTag("Wall"))
        {         
            Destroy(gameObject);
        }
    }
}
