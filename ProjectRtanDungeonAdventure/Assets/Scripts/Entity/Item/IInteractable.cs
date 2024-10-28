using UnityEngine;

public interface IInteractable
{
    public ItemData GetItemData();
    public void OnInteract();
    public void DisplayControll(Transform cameraTransform);
    public void SetUnactiveControll();
}
