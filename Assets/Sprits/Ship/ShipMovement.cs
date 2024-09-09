using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class ShipMovement : Player
{
    private void Update()
    {
        Vector3 newPos = InputManager.Instance.GetKeyMove(speed, transform.position);
        transform.DOMove(newPos, 0).SetEase(Ease.Linear);
    }
}
