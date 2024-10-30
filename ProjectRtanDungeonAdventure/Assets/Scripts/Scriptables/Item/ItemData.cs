using System;
using UnityEngine;

public enum EFFECTTYPE
{
    HealthRegen,
    StaminaRegen,
    HealthMax,
    StaminaMax,
    Speed,
    Jump,
    Dash,
}

public enum ITEMTYPE
{
    CONSUMABLE,
    EQUIPMENT
}

[Serializable]
public class EffectCell
{
    public EFFECTTYPE type;
    public float effectPower;
    public float effectTime;
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptables/Item")]
public class ItemData : ScriptableObject
{
    public int itemId;
    public string itemName;
    [TextArea] public string itemDesc;
    public GameObject itemPrefab;

    public ITEMTYPE type;

    [Header("Effect")]
    public EffectCell[] effectCells;
}