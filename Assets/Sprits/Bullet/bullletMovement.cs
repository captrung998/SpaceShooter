using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Xml.Serialization;

public class bullletMovement : MonoBehaviour
{
   
    public Vector3 newPosBullet;
    protected float speed = 5f;
    
    private void Update()
    {
        MoveBulletUp();
    }
    private void MoveBulletUp()
    {
        newPosBullet = transform.position;
        newPosBullet.y += speed * Time.deltaTime * 150;
        transform.DOMove(newPosBullet, 1).SetEase(Ease.Linear);
    }
   
}

