using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public Item item;
        string itemName;
        string itemDescription;
        Sprite itemSprite;
        public string id;
        public string itemId;
        public EVIDENCES itemType;
        [ContextMenu("Generate ID")] private void GenerateGuid() { id = System.Guid.NewGuid().ToString(); }

        [Header("UI Settings")]
        [SerializeField] Image itemImage;
        public GameObject itemSelected;

        [HideInInspector] public TMP_Text textName;
        [HideInInspector] public TMP_Text textDescription;
        [HideInInspector] public Image imagemDescription;

        public bool isFull;
        [HideInInspector] public bool isSelected;

        public void AddItem(Item _item)
        {
            Debug.Log("Entrou em AddItem em ItemSlot");

            item = _item;
            itemName = item._name;
            itemDescription = item.description;
            itemSprite = item.spriteIcon;
            isFull = true;
            itemImage.sprite = itemSprite;
            itemImage.gameObject.SetActive(true);
            itemId = item.id;
            itemType = item.evidenceType;

            Debug.Log("Atribuiu!");
            // Debug.Log($"ItemName: {itemName} | ItemDescription: {itemDescription} | ItemSprite: {itemSprite} | SlotFull: {isFull}");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) { OnLeftClick(); }
        }

        private void OnLeftClick()
        {
            InventoryManager.instance.DeselectAllSlots();

            itemSelected.SetActive(true);
            isSelected = true;

            if (isFull == true)
            {
                textName.text = null;
                textDescription.text = null;

                imagemDescription.sprite = itemSprite;
                imagemDescription.gameObject.SetActive(true);
                textName.text = itemName;
                textDescription.text = itemDescription;
            }
            else
            {
                imagemDescription.gameObject.SetActive(false);
                textName.text = null;
                textDescription.text = null;
            }
        }
    }
}