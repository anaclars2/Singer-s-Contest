using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    public Image image;

    public Item item;

    private void Start()
    {
        InitializeItem(item);
    }
    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }
}
