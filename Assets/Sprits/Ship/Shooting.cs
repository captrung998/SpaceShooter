using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private GameObject bulletPrefab3;
    [SerializeField] private Transform playerShip;
    [SerializeField] private AudioClip sounddShoot;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sounddShoot;
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        while (true)
        {
            if (InputManager.Instance.ClickLeftMouse())
            {
                if (ManagerController.Instance.currenScore < 10)
                {
                    Instantiate(bulletPrefab, playerShip.position, Quaternion.identity);
                    audioSource.Play();
                    yield return new WaitForSeconds(1f);
                }
                else if (ManagerController.Instance.currenScore > 20 && ManagerController.Instance.currenScore <= 30)
                {
                    Instantiate(bulletPrefab, playerShip.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, playerShip.position, Quaternion.identity);
                    audioSource.Play();
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    Instantiate(bulletPrefab, playerShip.position, Quaternion.identity);
                    Instantiate(bulletPrefab2, playerShip.position, Quaternion.identity);
                    Instantiate(bulletPrefab3, playerShip.position, Quaternion.identity);
                    audioSource.Play();
                    yield return new WaitForSeconds(1f);
                }

            }
            yield return null;
        }
    }
}
