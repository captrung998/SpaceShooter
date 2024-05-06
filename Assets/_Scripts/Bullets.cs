using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class Bullets : MonoBehaviour
{
    [SerializeField] Transform holderBullets;
    [SerializeField] GameObject bulletsPrefab;
    [SerializeField] GameObject playerShip;

    private float speedBullets = 0.5f;

    void Start()
    {
        DOTween.SetTweensCapacity(1000, 50);
        StartCoroutine(MoveBullets());
        StartCoroutine(SpawnBullets());
    }

    private IEnumerator SpawnBullets()
    {
        while (true)
        {
            bulletsPrefab.SetActive(true);
            var spawnBullets = Instantiate(bulletsPrefab, playerShip.transform.position, Quaternion.identity, holderBullets);
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator MoveBullets()
    {
        while (true)
        {
            Vector3 bulletPos = transform.position;
            bulletPos.y += speedBullets;
            transform.DOMoveY(bulletPos.y, 1);
            yield return new WaitForSeconds(0.001f);
        }

    }




}
