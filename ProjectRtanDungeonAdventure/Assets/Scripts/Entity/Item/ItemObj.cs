using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData itemData;

    public ItemData GetItemData() { return itemData; }

    public virtual void OnInteract()
    {
        foreach (EffectCell cell in itemData.effectCell)
        {
            EffectCell newCell = new EffectCell();
            newCell.type = cell.type;
            newCell.effectPower = cell.effectPower;
            newCell.effectTime = cell.effectTime;

            PlayerManager.Instance.playerInfo.AddEffect(newCell);
        }

        Destroy(gameObject);
    }
}
