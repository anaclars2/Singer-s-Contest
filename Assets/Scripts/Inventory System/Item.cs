using UnityEngine;

namespace InventorySystem
{
    public class Item : MonoBehaviour
    {
        [SerializeField] ItemData itemData;
        [HideInInspector] public string _name;
        [HideInInspector] public string description;
        [HideInInspector] public Sprite spriteIcon;

        private void Start()
        {
            _name = itemData._name;
            description = itemData.description;
            spriteIcon = itemData.spriteIcon;
        }
    }
}