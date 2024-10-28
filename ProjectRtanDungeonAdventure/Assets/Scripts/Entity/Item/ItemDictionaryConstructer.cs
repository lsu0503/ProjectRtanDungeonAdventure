using UnityEngine;

public class ItemDictionaryConstructer : GenericBaseDictionaryConstructer<ItemDictionary, ItemData>
{
    
}

public class ItemDictionary : BaseDictionary<ItemData>
{
    public int ConsumableNum = 5;
    public int EquipmentNum = 4;

    private void Awake()
    {
        GameManager.Instance.itemDict = this;
    }
}