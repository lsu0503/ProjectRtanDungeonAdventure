using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePoint;
    [SerializeField] private TextMeshProUGUI scoreGauge;

    private void Awake()
    {
        GameManager.Instance.gameOverUI = gameObject;
    }

    private void OnEnable()
    {
        scorePoint.text = GameManager.Instance.scoreLevel.ToString();
        scoreGauge.text = string.Format($"{GameManager.Instance.scoreExpCur / GameManager.Instance.scoreExpMax, 8:N5}");
    }

    public void RetryButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }

    public void TitleButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");
    }
}