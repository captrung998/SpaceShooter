using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpPlayer : MonoBehaviour
{
    [SerializeField] private float totalHealth;
    [SerializeField] private Image imageHealth;
    [SerializeField] private GameObject gameOverController;

    private float _health;
    private Tween animationTween = null;

    private void Start()
    {
        _health = totalHealth;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void ResetHealth()
    {
        _health = totalHealth;
        AnimationHealth();
    }

    public void AddHealth(float health)
    {
        _health += health;
        if (_health > totalHealth)
        {
            _health = totalHealth;
        }
        AnimationHealth();
    }

    public void SubHealth(float health)
    {
        _health -= health;
        if (_health <= 0)
        {
            _health = 0;
            EndGame(); // Call a method to end the game
        }
        AnimationHealth();
    }

    private void AnimationHealth()
    {
        float fillAmount = _health / totalHealth;

        if (animationTween != null)
        {
            animationTween.Kill();
        }

        animationTween = imageHealth.DOFillAmount(fillAmount, 0.2f).OnComplete(() =>
        {
            if (_health == 0)
            {
                EndGame(); // Call a method to end the game
            }
        });
    }

    private void EndGame()
    {
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.2f);
        gameOverController.SetActive(true);
        ScoreManager.Instance.GameOver();
        Time.timeScale = 0; // Pause the game after showing game over UI
    }
}
