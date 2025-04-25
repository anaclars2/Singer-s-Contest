using InventorySystem;
using SaveSystem;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] Image imagemDescription;

        bool menuActivated; // ver se menu ta ativado :D

        [Header("Slots Settings")]
        [SerializeField] GameObject containerSlot;
        [SerializeField] ItemSlot[] slots;
        public List<EVIDENCES> evidences;

        public static InventoryManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }
        }

        private void Start()
        {
            imagemDescription.gameObject.SetActive(false);
            textName.text = null;
            textDescription.text = null;

            if (containerSlot != null) { slots = containerSlot.GetComponentsInChildren<ItemSlot>(); }
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].textName = textName;
                slots[i].textDescription = textDescription;
                slots[i].imagemDescription = imagemDescription;
            }
        }

        private void Update()
        {
            #region Input
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && menuActivated == true)
            {
                inventoryMenu.SetActive(false);
                menuActivated = false;
            }
            else if (UnityEngine.Input.GetKeyDown(input))
            {
                menuActivated = !menuActivated;
                inventoryMenu.SetActive(menuActivated);
            }
            #endregion
        }

        public void AddItem(Item item)
        {
            Debug.Log($"Entrou em AddItem no InventoryManager\nslots.Length: {slots.Length}");

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isFull == false)
                {
                    slots[i].AddItem(item);
                    Debug.Log("AddItem do InventoryManager chamou AddItem do ItemSlot");
                    return;
                }
            }
        }

        private void GetAllEvidences()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].isFull == true && evidences.Contains(slots[i].itemType) == false)
                {
                    evidences.Add(slots[i].itemType);
                }
            }
        }

        public bool CheckIfContains(EVIDENCES evidence)
        {
            GetAllEvidences();
            if (evidences.Contains(evidence) == true) { Debug.Log($"Inventory contains item {evidence} necessary."); return true; }
            else { Debug.Log($"Inventory doesn't contains item {evidence} necessary."); return false; }
        }

        public void DeselectAllSlots()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].isSelected = false;
                slots[i].itemSelected.SetActive(false);
            }
        }
    }
}