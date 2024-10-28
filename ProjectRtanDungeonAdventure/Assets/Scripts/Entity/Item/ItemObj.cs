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
