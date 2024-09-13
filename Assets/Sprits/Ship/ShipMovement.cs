using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : Player
{
    private void Update()
    {
        Vector3 newPos = InputManager.Instance.GetKeyMove(speed, transform.position);
        transform.position = newPos;
    }
}
