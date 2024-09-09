using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ManagerController : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOver;
    public float countTime = 0;
    public int currenScore = 0;
    private static ManagerController instance;
    float maxValue, minute, second;
    public static ManagerController Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        StartCoroutine(CountTime());
        score.text = currenScore.ToString();
    }

    public void UpdateBar(float currenValue, float maxValue)
    {
        fillBar.fillAmount = currenValue / maxValue;
        valueText.text = currenValue.ToString() + " / " + maxValue.ToString();
    }

    public void UpdateScore(int addScore)
    {
        currenScore += addScore;
        score.text = currenScore.ToString();
    }

    public IEnumerator CountTime()
    {
        while (true)
        {
            countTime++;
            minute = Mathf.Floor(countTime / 60);  // Minutes
            second = countTime % 60;  // Seconds
            // Display minutes and seconds in the format "MM:SS"
            time.text = string.Format("{0:00}:{1:00}", minute, second);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;// Pause the game
        gameOverText.text = "Game Over\nScore: " + currenScore;
    }

  


}
