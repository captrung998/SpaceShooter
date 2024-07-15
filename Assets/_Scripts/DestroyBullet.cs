using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public AudioClip explosionSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            ScoreManager.Instance.AddScore(10);

            if (audioSource != null) // Check if audioSource is not null
            {
                audioSource.PlayOneShot(explosionSound);
            }
        }
    }
}
