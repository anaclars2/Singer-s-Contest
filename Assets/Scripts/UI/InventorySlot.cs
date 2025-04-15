using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    private InventoryItemSO currentItem;
    private InventoryManager manager;
    public GameObject highlight;

    public void Setup(InventoryItemSO item, InventoryManager inventoryManager)
    {
        currentItem = item;
        manager = inventoryManager;
        iconImage.sprite = item.icon;
        iconImage.color = Color.white;
        iconImage.enabled = true;

        Debug.Log("Sprite atribuído ao slot: " + item.icon);

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(onClick);
        
    }

    private void onClick()
    {
        manager.ShowItem(currentItem, this);
        manager.HighlightedSlot(this);

    }

    public void SetHighlight(bool isActive)
    {
        if(highlight != null)
        {
            highlight.SetActive(isActive);
        }
    }

  
}
