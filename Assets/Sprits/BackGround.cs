using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround: MonoBehaviour
{
    public float speed = 2.0f;
    public Transform player;
    public Transform[] backgrounds; // Các background để lặp lại
    public float backgroundWidth;   // Chiều rộng của mỗi background

    void Update()
    {
        Vector3 playerDirection = GetPlayerDirection();
        MoveBackground(playerDirection * speed * Time.deltaTime);
        LoopBackground();
    }

    Vector3 GetPlayerDirection()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.left;
        }

        return direction.normalized;
    }

    void MoveBackground(Vector3 movement)
    {
        foreach (Transform background in backgrounds)
        {
            background.position += movement;
        }
    }

    void LoopBackground()
    {
        foreach (Transform background in backgrounds)
        {
            if (background.position.x < -backgroundWidth)
            {
                Vector3 rightmostPosition = GetRightmostBackground().position;
                background.position = new Vector3(rightmostPosition.x + backgroundWidth, background.position.y, background.position.z);
            }

            if (background.position.x > backgroundWidth)
            {
                Vector3 leftmostPosition = GetLeftmostBackground().position;
                background.position = new Vector3(leftmostPosition.x - backgroundWidth, background.position.y, background.position.z);
            }

            if (background.position.y < -backgroundWidth)
            {
                Vector3 topmostPosition = GetTopmostBackground().position;
                background.position = new Vector3(background.position.x, topmostPosition.y + backgroundWidth, background.position.z);
            }

            if (background.position.y > backgroundWidth)
            {
                Vector3 bottommostPosition = GetBottommostBackground().position;
                background.position = new Vector3(background.position.x, bottommostPosition.y - backgroundWidth, background.position.z);
            }
        }
    }

    Transform GetRightmostBackground()
    {
        Transform rightmost = backgrounds[0];
        foreach (Transform background in backgrounds)
        {
            if (background.position.x > rightmost.position.x)
            {
                rightmost = background;
            }
        }
        return rightmost;
    }

    Transform GetLeftmostBackground()
    {
        Transform leftmost = backgrounds[0];
        foreach (Transform background in backgrounds)
        {
            if (background.position.x < leftmost.position.x)
            {
                leftmost = background;
            }
        }
        return leftmost;
    }

    Transform GetTopmostBackground()
    {
        Transform topmost = backgrounds[0];
        foreach (Transform background in backgrounds)
        {
            if (background.position.y > topmost.position.y)
            {
                topmost = background;
            }
        }
        return topmost;
    }

    Transform GetBottommostBackground()
    {
        Transform bottommost = backgrounds[0];
        foreach (Transform background in backgrounds)
        {
            if (background.position.y < bottommost.position.y)
            {
                bottommost = background;
            }
        }
        return bottommost;
    }
}