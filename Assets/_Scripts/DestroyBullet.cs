using System;
using System.Collections;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ScoreManager.Instance.AddScore(10);

            if (audioSource != null) // Check if audioSource is not null
            {
                audioSource.Play();
            }
            //StartCoroutine(DelayDestroy(other.gameObject));
        }
    }

    private IEnumerator DelayDestroy(GameObject collision)
    {

        yield return new WaitForSeconds(0.2f); 
        Destroy(this.gameObject);
        Destroy(collision);
    }
}
