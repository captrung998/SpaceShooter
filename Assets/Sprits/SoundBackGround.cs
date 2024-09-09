using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBackGround : MonoBehaviour
{
    [SerializeField] private AudioClip soundBackGround;
    private AudioSource audioSource;
    [SerializeField] private GameObject gameOver;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundBackGround;
        audioSource.Play();
    }

    private void Update()
    {
       
        if (gameOver.activeSelf == true)
        {
            audioSource.Stop();
        }
    }



}
