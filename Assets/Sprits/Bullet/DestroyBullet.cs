using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private float timeDestroy = 5f;
    [SerializeField] private AudioClip soundExplosion;
    ManagerController controller;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundExplosion;
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        while (true)
        {
            Destroy(gameObject, timeDestroy);
            yield return new WaitForSeconds(timeDestroy);
        }
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
            audioSource.transform.SetParent(null);
            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(gameObject, audioSource.clip.length);
            ManagerController.Instance.UpdateScore(1);
        }
    }
}
