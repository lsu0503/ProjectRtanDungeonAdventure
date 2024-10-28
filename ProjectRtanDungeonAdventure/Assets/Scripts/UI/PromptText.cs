using TMPro;
using UnityEngine;

public class PromptText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameMesh;
    [SerializeField] private TextMeshProUGUI descMesh;

    public void SetPromptText(ItemData itemData)
    {
        nameMesh.text = itemData.itemName;
        descMesh.text = itemData.itemDesc;
        gameObject.SetActive(true);
    }

    public void UnsetPrompt()
    {
        gameObject.SetActive(false);
    }
}