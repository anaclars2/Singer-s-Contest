using UnityEngine;

namespace InventorySystem
{
    public class Item : MonoBehaviour
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

        [HideInInspector] public bool collected = false;
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

        public void RemoveFromScene()
        {
            visual.gameObject.SetActive(false);
            _collider.enabled = false;
        }
    }
}