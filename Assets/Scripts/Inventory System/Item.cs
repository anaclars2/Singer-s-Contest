using SaveSystem;
using UnityEngine;

namespace InventorySystem
{
    public class Item : MonoBehaviour, IDataPersistence
    {
        [SerializeField] ItemData itemData;
        [HideInInspector] public string _name;
        [HideInInspector] public string description;
        [HideInInspector] public Sprite spriteIcon;
        [HideInInspector] public bool collected = false;
        [SerializeField] private string id;

        [ContextMenu("Generate ID")] private void GenerateGuid() { id = System.Guid.NewGuid().ToString(); }

        private void Start()
        {
            _name = itemData._name;
            description = itemData.description;
            spriteIcon = itemData.spriteIcon;
        }

        public void LoadData(GameData data)
        {
            data.collectedItems.TryGetValue(id, out collected);
            if (collected == true)
            {
                InventoryManager.instance.AddItem(this);
                Destroy(this.gameObject);
            }
        }

        public void SaveData(GameData data)
        {
            // se tiver ja no dicionario entao removemos
            // e adicionamos denovo para nao dar erro
            if (data.collectedItems.ContainsKey(id)) { data.collectedItems.Remove(id); }
            data.collectedItems.Add(id, collected);
        }
    }
}