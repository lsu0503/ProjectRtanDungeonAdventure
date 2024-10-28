using JetBrains.Annotations;
using System;
using UnityEngine;

public enum EFFECTTYPE
{
    HEALTH,
    STAMINA,
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

    [Header("Effect")]
    public EffectCell[] effectCell;
}
