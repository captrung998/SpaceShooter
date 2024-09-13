using System.Collections;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private AudioClip soundExplosion;
    [SerializeField] protected GameObject explosion;

    private AudioSource audioSource;
    private bool hasCollided = false; // Flag to track if collision has occurred

    private void Start()
    {
        // Try to get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If it doesn't exist, add the AudioSource component
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the explosion sound to the AudioSource
        audioSource.clip = soundExplosion;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided)
            return; // If the bullet has already collided, do nothing

        if (other.CompareTag("Enemy"))
        {
            hasCollided = true;
            audioSource.Play();
            Destroy(other.gameObject, audioSource.clip.length);            
            Destroy(gameObject, audioSource.clip.length / 8);
            ManagerController.Instance.UpdateScore(1);

        }
    }
}
