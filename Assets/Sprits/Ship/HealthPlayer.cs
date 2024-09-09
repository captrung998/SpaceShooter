using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Player
{
    float maxHealth = health;
    float currenHealth = health;

    private void Start()
    {
        ManagerController.Instance.UpdateBar(currenHealth, maxHealth);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currenHealth > 0)
            {
                currenHealth -= 10f;
                ManagerController.Instance.UpdateBar(currenHealth, maxHealth);
                Destroy(other.gameObject);
            }
            else if (currenHealth == 0)
                ManagerController.Instance.GameOver();

        }
    }
}
