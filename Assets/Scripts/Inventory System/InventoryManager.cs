using NUnit.Framework.Interfaces;
using TMPro;
using UISystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Progress;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] KeyCode input;

        [Header("UI Settings")]
        public GameObject inventoryMenu;
        [SerializeField] TMP_Text textName;
        [SerializeField] TMP_Text textDescription;

        bool menuActivated; // ver se menu ta ativado :D

        [Header("Slots Settings")]
        [SerializeField] GameObject containerSlot;
        [SerializeField] ItemSlot[] slots;

        public static InventoryManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            if (containerSlot != null) { slots = containerSlot.GetComponentsInChildren<ItemSlot>(); }
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].textName = textName;
                slots[i].textDescription = textDescription;
            }
        }

        private void Update()
        {
            #region Input
            if (UnityEngine.Input.GetKeyDown(input))
            {
                menuActivated = !menuActivated;
                inventoryMenu.SetActive(menuActivated);
            }
            #endregion
        }

        public void AddItem(Item item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isFull == false)
                {
                    slots[i].AddItem(item);
                    return;
                }
            }
        }

        public void DeselectAllSlots()
        {
            Debug.Log("bbbbbbbbbb");
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].itemSelected.SetActive(false);
                slots[i].isSelected = false;
                return;
            }
        }
    }
}