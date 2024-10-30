using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject bearText;
    [SerializeField] private GameObject ballText;
    [SerializeField] private float changeTime;
    private float timer;

    private void Start()
    {
        ballText.SetActive(false);
        bearText.SetActive(true);
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > changeTime)
        {
            timer = 0;
            ballText.SetActive(!ballText.activeSelf);
            bearText.SetActive(!bearText.activeSelf);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
