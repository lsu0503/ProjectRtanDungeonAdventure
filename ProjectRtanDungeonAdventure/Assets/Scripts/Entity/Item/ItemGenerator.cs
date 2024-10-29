using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private float generateRate;
    [SerializeField] private float generateRange;
    private float generateTime;

    private void FixedUpdate()
    {
        if(Time.time - generateTime > generateRate / (1.0 + (0.2 * GameManager.Instance.scoreLevel)))
        {
            generateTime = Time.time;

            GenerateItem(Random.Range(0, GameManager.Instance.itemDict.ConsumableNum));
        }
    }

    private void GenerateItem(int itemId)
    {
        ItemData itemData = GameManager.Instance.itemDict.GetDict(itemId.ToString());

        float generateAngle = Random.Range(0.0f, 360.0f);
        float generateDistance = Random.Range(0.0f, generateRange);

        Vector3 generatePosition = Quaternion.Euler(0.0f, generateAngle, 0.0f) * new Vector3(generateDistance, 8.0f, 0.0f);
        Vector3 generatePositionWorld = generatePosition + transform.position;

        Instantiate(itemData.itemPrefab, generatePositionWorld, Quaternion.identity);
    }
}
