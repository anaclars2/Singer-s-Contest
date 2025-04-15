using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    [TextArea(2, 5)]
    public string description;
}
