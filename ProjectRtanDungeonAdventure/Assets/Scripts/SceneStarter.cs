using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [SerializeField] SCENE currentScene;

    private void Awake()
    {
        GameManager.Instance.currentScene = currentScene;
    }

    private void Start()
    {
        GameManager.Instance.InitGame();
        Destroy(gameObject);
    }
}