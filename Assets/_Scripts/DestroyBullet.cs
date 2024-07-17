using System.Collections;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            ScoreManager.Instance.AddScore(10);

            if (audioSource != null) // Check if audioSource is not null
            {
                audioSource.Play();
            }
            StartCoroutine(DelayDestroy(collision.gameObject));
        }
    }

    private IEnumerator DelayDestroy(GameObject collision)
    {

        yield return new WaitForSeconds(0.5f); 
        Destroy(this.gameObject);
        Destroy(collision);
    }
}
