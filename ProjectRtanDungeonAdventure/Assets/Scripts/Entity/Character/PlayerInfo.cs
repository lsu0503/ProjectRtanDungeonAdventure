using System;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

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

    [SerializeField] private ItemData equipment;

    private void Awake()
    {
        GameManager.Instance.playerInfo = this;
    }

    protected override void Start()
    {
        base.Start();
        stamina.Initailize();
        gem.Initailize();

        if(equipment != null)
            EquipItem(equipment);
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

        AddAttribute(cell);
    }

    public void EndEffect(EffectCell cell)
    {
        SubstactAttribute(cell);

        effects.Remove(cell);
    }

    public void EquipItem(ItemData item)
    {
        equipment = item;

        foreach(EffectCell cell in item.effectCells)
        {
            AddAttribute(cell);
        }
    }

    public ItemData UnEquipItem()
    {
        foreach (EffectCell cell in equipment.effectCells)
        {
            SubstactAttribute(cell);
        }

        ItemData resultData = equipment;
        equipment = null;
        return resultData;
    }

    public ItemData TryEquip(ItemData item)
    {
        ItemData resultItem = null;
        
        if(equipment != null)
            resultItem = UnEquipItem();
        EquipItem(item);

        return resultItem;
    }

    private void AddAttribute(EffectCell cell)
    {
        switch (cell.type)
        {
            case EFFECTTYPE.HealthRegen:
                health.recover += cell.effectPower;
                break;

            case EFFECTTYPE.StaminaRegen:
                stamina.recover += cell.effectPower;
                break;

            case EFFECTTYPE.HealthMax:
                health.max += (int)cell.effectPower;

                if (cell.effectPower > 0)
                    health.current += (int)cell.effectPower;

                else if (health.current > health.max)
                    health.current = health.max;

                break;

            case EFFECTTYPE.StaminaMax:
                stamina.max += (int)cell.effectPower;

                if (cell.effectPower > 0)
                    stamina.current += (int)cell.effectPower;

                else if (stamina.current > stamina.max)
                    stamina.current = stamina.max;

                break;

            case EFFECTTYPE.Speed:
                moveSpeed += cell.effectPower;
                break;

            case EFFECTTYPE.Jump:
                jumpPowerOnGround += cell.effectPower;
                jumpPowerInAir += cell.effectPower * 0.65f;
                break;

            case EFFECTTYPE.Dash:
                dashSpeedOnGround += cell.effectPower;
                dashSpeedInAir += cell.effectPower * 1.25f;
                break;
        }
    }

    private void SubstactAttribute(EffectCell cell)
    {
        switch (cell.type)
        {
            case EFFECTTYPE.HealthRegen:
                health.recover -= cell.effectPower;
                break;

            case EFFECTTYPE.StaminaRegen:
                stamina.recover -= cell.effectPower;
                break;

            case EFFECTTYPE.HealthMax:
                health.max += (int)cell.effectPower;

                if (cell.effectPower < 0)
                    health.current -= (int)cell.effectPower;

                else if (health.current < health.max)
                    health.current = health.max;

                break;

            case EFFECTTYPE.StaminaMax:
                stamina.max -= (int)cell.effectPower;

                if (cell.effectPower < 0)
                    stamina.current += (int)cell.effectPower;

                else if (stamina.current < stamina.max)
                    stamina.current = stamina.max;

                break;

            case EFFECTTYPE.Speed:
                moveSpeed -= cell.effectPower;
                break;

            case EFFECTTYPE.Jump:
                jumpPowerOnGround -= cell.effectPower;
                jumpPowerInAir -= cell.effectPower * 0.65f;
                break;

            case EFFECTTYPE.Dash:
                dashSpeedOnGround -= cell.effectPower;
                dashSpeedInAir -= cell.effectPower * 1.25f;
                break;
        }
    }
}
