using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

}

