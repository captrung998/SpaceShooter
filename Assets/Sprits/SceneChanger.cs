using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private AudioClip soundBackGround;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundBackGround;
        audioSource.Play();
    }
    // Method to change the scene
    public void ChangeScene(string sceneName)
    {
        audioSource.Stop();
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
        audioSource.Stop();
        Application.Quit();
        Debug.Log("Game is quitting...");

    }
}

