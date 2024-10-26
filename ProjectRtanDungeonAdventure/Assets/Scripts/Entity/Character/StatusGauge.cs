using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StatusGauge
{
    public int max;
    public int current;
    public float recover;
    public float timer;

    [SerializeField] private Image? uiBar;

    public virtual void Initailize()
    {
        current = max;
        UpdateUI();
    }

    public void Recover()
    {
        timer += recover * Time.deltaTime;

        if (timer >= 1.0f)
        {
            Add((int)(timer / 1.0f));
            timer = timer % 1.0f;

            UpdateUI();
        }
    }

    public virtual void Add(int amount)
    {
        current += amount;

        if(current > max)
            current = max;

        UpdateUI();
    }

    public virtual bool Substract(int amount)
    {
        if (current < amount)
            return false;

        else
        {
            current -= amount;
            UpdateUI();
            return true;
        }
    }

    protected virtual void UpdateUI()
    {
        if (uiBar != null)
            uiBar.fillAmount = current / (float)max;
    }
}
