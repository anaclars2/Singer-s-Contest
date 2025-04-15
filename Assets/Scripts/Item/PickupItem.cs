using UnityEngine;
using UnityEngine.Rendering;

public class PickupItem : MonoBehaviour
{
    public InventoryItemSO itemData;

    public void interact()
    {
        InventoryManager inventory = FindFirstObjectByType<InventoryManager>();

        if (inventory != null && itemData !=null)
        {
            inventory.AddItem(itemData);
            Debug.Log("Item coletado: " + itemData.name);
            Destroy(gameObject);
            Debug.Log("DESTROY chamado no objeto: " + gameObject.name);

        }
        else
        {
            Debug.LogWarning("itemData ou InventoryManager está nulo");
        }
    }
}
