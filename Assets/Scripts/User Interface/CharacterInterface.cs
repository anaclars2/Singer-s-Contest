using AudioSystem;
using UnityEngine;

namespace UISystem
{
    [CreateAssetMenu(fileName = "New CharacterInterface", menuName = "Scriptable Objects/CharacterInterface")]
    public class CharacterInterface : ScriptableObject
    {
        public MUSIC musicType;

        [Header("Scene Settings")]
        public Sprite background;
        public Sprite track;
        public Sprite blur;

        [Header("Characters Settings")]
        public Sprite characterLeft;
        public Sprite nameCharacterLeft;
        public Sprite backgroundCharacterLeft;

        public Sprite characterRight;
        public Sprite nameCharacterRight;
        public Sprite backgroundCharacterRight;

        [Header("Progress Bar Settings")]
        public Sprite iconProgressBar;
        public Sprite backgroundProgressBar;
        public Sprite fillProgressBar;

        [Header("Arrow")]
        public GameObject prefabUpArrow;
        public GameObject prefabDownArrow;
        public GameObject prefabLeftArrow;
        public GameObject prefabRightArrow;

        [Header("Button")]
        public Sprite buttonUpArrow;
        public Sprite buttonDownArrow;
        public Sprite buttonLeftArrow;
        public Sprite buttonRightArrow;
    }
}