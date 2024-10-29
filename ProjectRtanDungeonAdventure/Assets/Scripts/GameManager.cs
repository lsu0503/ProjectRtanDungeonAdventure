using System;
using UnityEngine;

public enum SCENE
{
    TITLESCENE,
    MAINSCENE
}

public class GameManager: GenericSingleton<GameManager>
{
    public PlayerInfo playerInfo;
    public ItemDictionary itemDict;
    public GameObject gameOverUI;

    public int scoreLevel;
    public float scoreExpMax;
    public float scoreExpCur;

    public float trainCalm;

    public int timeStack;
    public float timeGauge;

    public bool isMouseLocked;

    public SCENE currentScene;

    private void Start()
    {
        InitGame();
    }

    private void FixedUpdate()
    {
        if(currentScene == SCENE.MAINSCENE)
        {
            ProgressGame();
        }
    }

    public void InitGame()
    {
        scoreLevel = 0;
        scoreExpMax = 10.0f;
        scoreExpCur = 0.0f;

        trainCalm = 0.0f;

        timeStack = 9;
        timeGauge = 20.0f;

        Cursor.lockState = CursorLockMode.Locked;
        isMouseLocked = true;

        if(gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    private void ProgressGame()
    {
        if(trainCalm > 0.0f)
            trainCalm -= Time.deltaTime;

        timeGauge -= Time.deltaTime;

        if(timeGauge < 0.0f)
        {
            if (timeStack > 0)
            {
                timeStack--;
                timeGauge += 20.0f;
            } 

            else
                GameOver();
        }    
    }

    public void GetScore(int amount)
    {
        scoreExpCur += amount;

        while (scoreExpCur > scoreExpMax)
        {
            scoreExpCur -= scoreExpMax;
            scoreExpMax *= 1.2f;
            scoreLevel++;
        }
    }

    public void GetTrainCalm(float amount)
    {
        trainCalm += amount;
        if (trainCalm > 40.0f)
            trainCalm = 40.0f;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        isMouseLocked = false;

        gameOverUI.SetActive(true);
    }
}
