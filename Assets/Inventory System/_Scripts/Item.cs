using UnityEngine;
using SaveSystem;

namespace InventorySystem
{
    public class Item : MonoBehaviour, IDataPersistence
    {
        [SerializeField] ItemData itemData;
        [HideInInspector] public string _name;
        [HideInInspector] public string description;
        [HideInInspector] public Sprite spriteIcon;
        [HideInInspector] public EVIDENCES evidenceType;
        [HideInInspector] public string group;
        [HideInInspector] public bool isCollectible;
        [HideInInspector] public string idea;

        [HideInInspector] public ItemSlot slot;
        [SerializeField] GameObject visual;
        [SerializeField] Collider _collider;
        [SerializeField] public string flagOnCollect;

        public bool collected = false;
        public string id;

        [ContextMenu("Generate ID")] private void GenerateGuid() { id = System.Guid.NewGuid().ToString(); }

        private void Start()
        {
            _name = itemData._name;
            description = itemData.description;
            spriteIcon = itemData.spriteIcon;
            evidenceType = itemData.evidenceType;
            group = itemData.group;
            isCollectible = itemData.isCollectible;
            idea = itemData.idea;
        }

        public void LoadData(GameData data)
        {
            data.collectedItems.TryGetValue(id, out collected);
            if (collected == true)
            {
                RemoveFromScene();
            }
        }

        public void SaveData(GameData data)
        {
            // se tiver ja no dicionario entao removemos
            // e adicionamos denovo para nao dar erro
            if (data.collectedItems.ContainsKey(id)) { data.collectedItems.Remove(id); }
            data.collectedItems.Add(id, collected);
            Debug.Log("WELCOME SAVE TS15");

        }

        public void RemoveFromScene()
        {
            visual.gameObject.SetActive(false);
            _collider.enabled = false;
        }
    }
}