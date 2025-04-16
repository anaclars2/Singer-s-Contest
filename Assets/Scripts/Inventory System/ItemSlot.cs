using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        string itemName;
        string itemDescription;
        Sprite itemSprite;

        [Header("UI Settings")]
        [SerializeField] Image itemImage;
        public GameObject itemSelected;

        [HideInInspector] public TMP_Text textName;
        [HideInInspector] public TMP_Text textDescription;

        [HideInInspector] public bool isFull;
        [HideInInspector] public bool isSelected;

        public void AddItem(Item item)
        {
            itemName = item._name;
            itemDescription = item.description;
            itemSprite = item.spriteIcon;
            isFull = true;
            itemImage.sprite = itemSprite;
            itemImage.gameObject.SetActive(true);

            // Debug.Log($"ItemName: {itemName} | ItemDescription: {itemDescription} | ItemSprite: {itemSprite} | SlotFull: {isFull}");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) { OnLeftClick(); }
            if (eventData.button == PointerEventData.InputButton.Right) { OnRightClick(); }
        }

        public void OnLeftClick()
        {
            InventoryManager.instance.DeselectAllSlots();
            Debug.Log("aaaaaaaaa");

            itemSelected.SetActive(true);
            isSelected = true;

            textName.text = itemName;
            textDescription.text = itemDescription;
        }

        public void OnRightClick()
        {
            //  itemSelected.SetActive(false);
            //  isSelected = false;
        }
    }
}