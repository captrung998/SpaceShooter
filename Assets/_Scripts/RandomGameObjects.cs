using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class RandomGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects; // Array of game objects to activate
    private float cooldown = 1f;
    private int time = 0;
    private int upTime = 10;


    private void Update()
    {
        //Debug.Log(cooldown);
    }

    private void Start()
    {
        if (gameObjects.Length > 0)
        {
            StartCoroutine(ActivateRandomObjects());
            StartCoroutine(TimeCount());
        }
    }

    private IEnumerator ActivateRandomObjects()
    {

        while (true)
        {

            int randomIndex = Random.Range(0, gameObjects.Length);
            GameObject selectedObject = gameObjects[randomIndex];
            selectedObject.SetActive(true);
            if (time == upTime)
            {
                cooldown -= 0.01f;
                yield return new WaitForSeconds(cooldown);
                upTime += 100;
                
            }
            yield return new WaitForSeconds(cooldown);


            selectedObject.SetActive(false);

        }
    }


    private IEnumerator TimeCount()
    {
        while (true)
        {

            yield return new WaitForSeconds(1f);
            time++;

        }
    }
}
