using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    public Vector3 GetKeyMove(float speed, Vector3 ship)
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = ship;

        newPosition.x += horizontalInput * speed * Time.deltaTime;
        newPosition.y += VerticalInput * speed * Time.deltaTime;
       
        return newPosition;
    }
    public bool ClickLeftMouse() => Input.GetMouseButton(0);

}
