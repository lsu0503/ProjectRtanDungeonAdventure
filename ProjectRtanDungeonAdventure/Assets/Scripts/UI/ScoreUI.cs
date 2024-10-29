using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Image scoreGauge;
    [SerializeField] private TextMeshProUGUI scorePoint;

    public void Update()
    {
        scorePoint.text = GameManager.Instance.scoreLevel.ToString();
        scoreGauge.fillAmount = GameManager.Instance.scoreExpCur / GameManager.Instance.scoreExpMax;
    }
}
