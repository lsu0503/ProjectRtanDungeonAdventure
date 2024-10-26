using System;
using TMPro;

[Serializable]
public class StatusPoint : StatusGauge
{
    public int pointMax;
    public int point;
    public TextMeshProUGUI pointText;

    public override void Initailize()
    {
        point = pointMax;
        current = 0;
        UpdateUI();
    }

    public override void Add(int amount)
    {
        current += amount;

        if(current >= max)
        {
            point++;
            current -= max;
        }

        UpdateUI();
    }

    public override bool Substract(int amount)
    {
        if(current < amount)
        {
            if(point <= 0)
                current = 0;

            else
            {
                point--;
                current += max;
            }
        }

        current -= amount;
        UpdateUI();
        return true;
    }

    public bool SpendPoint(int amount)
    {
        if (point < amount)
            return false;

        else
        {
            point -= amount;
            UpdateUI();
            return true;
        }
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();
        pointText.text = point.ToString();
    }
}