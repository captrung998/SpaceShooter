using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private GameObject playerShip;

    private float speed = 0.05f;
    public float hp = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveShip());
    }
    private IEnumerator MoveShip()
    {
        while (true)
        {
            float leftRight = Input.GetAxisRaw("Horizontal");
            float upDown = Input.GetAxisRaw("Vertical");

            if (leftRight != 0 || upDown != 0)
            {
                if (leftRight != 0 && upDown != 0)
                {
                    speed /= 6;
                    playerShip.transform.position += new Vector3(leftRight * speed, upDown * speed, 0f);
                    speed *= 6;

                }
                playerShip.transform.position += new Vector3(leftRight * speed, upDown * speed, 0f);
            }
            yield return new WaitForFixedUpdate();
        }
    }


}
