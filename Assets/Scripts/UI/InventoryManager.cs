using UnityEngine;
using TMPro;
using NUnit.Framework;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [Header("Prefabs e Containers")]
    public GameObject inventorySlotPrefab;
    public Transform itemContainer;

    private InventorySlot currentSelectedSlot;


    public Image itemBigImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescText;

    public List<InventorySlot> slotList;
    public List<InventoryItemSO> items; 
    

    public void Start()
    {
        PopulateInventory();
    }

    public void AddItem(InventoryItemSO newItem)
    {
        items.Add(newItem);
        Debug.Log("Adicionado ao inventário: " + newItem.name + " | Sprite: " + newItem.icon);
        PopulateInventory();
    }

    void PopulateInventory()
    {
        for (int i = 0; i < slotList.Count; i++)
        {

            if (i < items.Count)
            {
                slotList[i].Setup(items[i], this);
            }
            else
            {
                slotList[i].gameObject.SetActive(false);
            }
        }
    }

    public void ShowItem(InventoryItemSO item, InventorySlot slot)
    {
        itemBigImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemDescText.text = item.description;

        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.SetHighlight(false);

            currentSelectedSlot = slot;
            currentSelectedSlot.SetHighlight(true);
        }
    }

    public void HighlightedSlot(InventorySlot selectedSlot)
    {
        foreach (var slot in slotList)
        {
            slot.SetHighlight(slot == selectedSlot);
        }
    }
    public void SetSelectedSlot(InventorySlot newSlot)
    {
        if (currentSelectedSlot != null)
        {
            currentSelectedSlot.SetHighlight(false);

            currentSelectedSlot = newSlot;
            currentSelectedSlot.SetHighlight(true);
        }
    }
}
