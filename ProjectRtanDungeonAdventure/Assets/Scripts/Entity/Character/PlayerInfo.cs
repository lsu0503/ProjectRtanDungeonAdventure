using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : CharacterInfo
{
    public StatusGauge stamina;
    public StatusPoint gem;

    public float sprintCostRate;
    public float sprintSpeed;
    public bool isOnSprint;

    public float jumpPowerOnGround;
    public float jumpPowerInAir;
    public int jumpCostOnGound;
    public int jumpCostInAir;

    public float dashSpeedOnGround;
    public float dashSpeedInAir;
    public int dashCostOnGround;
    public int dashCostInAir;
    public float dashTimeOnGround;
    public float dashTimeInAir;

    [SerializeField] private List<EffectCell> effects = new List<EffectCell>();

    private void Awake()
    {
        PlayerManager.Instance.playerInfo = this;
    }

    protected override void Start()
    {
        base.Start();
        stamina.Initailize();
        gem.Initailize();
    }

    protected override void FixedUpdate()
    {
        float timeProgress = Time.deltaTime;

        base.FixedUpdate();

        if(isOnGround && !isOnSprint)
            stamina.Recover();

        gem.Recover();

        for(int i = 0; i < effects.Count; i++)
        {
            effects[i].effectTime -= timeProgress;

            if (effects[i].effectTime <= 0)
                EndEffect(effects[i]);
        }
    }

    public void AddEffect(EffectCell cell)
    {
        effects.Add(cell);

        switch (cell.type)
        {
            case EFFECTTYPE.HEALTH:
                health.recover += cell.effectPower;
                break;

            case EFFECTTYPE.STAMINA:
                stamina.recover += cell.effectPower;
                break;
        }
    }

    public void EndEffect(EffectCell cell)
    {
        switch(cell.type)
        {
            case EFFECTTYPE.HEALTH:
                health.recover -= cell.effectPower;
                break;

            case EFFECTTYPE.STAMINA:
                stamina.recover -= cell.effectPower;
                break;
        }

        effects.Remove(cell);
    }
}