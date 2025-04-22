using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public string _name;
        public Sprite sprite;
        [TextArea] public string[] text;
    }
}