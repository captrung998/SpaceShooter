using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    private PlayerShip player;

    private static ScoreManager _instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    //public TextMeshProUGUI gameOverText;

    void Start()
    {
        Time.timeScale = 0.5f;
        StartCoroutine(ScoreText());
    }


    private IEnumerator ScoreText()
    {
        while (true)
        {
            scoreText.text = "Score: " + score ;
            yield return new WaitForEndOfFrame();
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;

    }
    public void GameOver()
    {
        Time.timeScale = 0;// Pause the game
        gameOverText.text = "Game Over\nScore: " + score;
    }
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<ScoreManager>();
                    singletonObject.name = typeof(ScoreManager).ToString() + " (Singleton)";
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

   
}
