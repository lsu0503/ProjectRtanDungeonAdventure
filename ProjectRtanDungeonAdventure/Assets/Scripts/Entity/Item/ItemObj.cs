using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private GameObject CommandCanvas;
    private Transform? cameraTransform;

    private void Update()
    {
        if(cameraTransform != null)
            CommandCanvas.transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }

    public ItemData GetItemData() { return itemData; }

    public virtual void OnInteract()
    {
        if (itemData.type == ITEMTYPE.CONSUMABLE)
        {
            GameManager.Instance.GetScore(1);

            foreach (EffectCell cell in itemData.effectCells)
            {
                EffectCell newCell = new EffectCell();
                newCell.type = cell.type;
                newCell.effectPower = cell.effectPower;
                newCell.effectTime = cell.effectTime;

                GameManager.Instance.playerInfo.AddEffect(newCell);
            }

            Destroy(gameObject);
        }

        else if(itemData.type == ITEMTYPE.EQUIPMENT)
        {
            ItemData newItem = GameManager.Instance.playerInfo.TryEquip(itemData);

            if (newItem != null)
                Instantiate(newItem.itemPrefab, transform.position + Vector3.up * 1.5f, transform.rotation);

            Destroy(gameObject);
        }
    }

    public void DisplayControll(Transform _cameraTransform)
    {
        cameraTransform = _cameraTransform;
        CommandCanvas.SetActive(true);
    }

    public void SetUnactiveControll()
    {
        CommandCanvas.SetActive(false);
        cameraTransform = null;
    }
}