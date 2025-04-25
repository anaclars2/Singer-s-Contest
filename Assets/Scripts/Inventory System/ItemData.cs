using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Objects/Item")]
    public class ItemData : ScriptableObject
    {
        public string _name;
        [TextArea] public string description;
        public Sprite spriteIcon;
        public EVIDENCES evidenceType;
    }
}