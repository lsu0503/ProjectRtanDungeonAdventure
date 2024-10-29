using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressUI : MonoBehaviour
{
    [SerializeField] private Image trainCalmGauge;
    [SerializeField] private Image timeGaugeImg;
    [SerializeField] private TextMeshProUGUI timePointText;

    private void Update()
    {
        trainCalmGauge.fillAmount = GameManager.Instance.trainCalm / 40.0f;
        timeGaugeImg.fillAmount = GameManager.Instance.timeGauge / 20.0f;
        timePointText.text = GameManager.Instance.timeStack.ToString();
    }
}
