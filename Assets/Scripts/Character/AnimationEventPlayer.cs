using UnityEngine;
using UnityEngine.Windows;

namespace CharacterSystem
{
    public class AnimationEventPlayer : MonoBehaviour
    {
        [SerializeField] Player player;

        private void Start() { if (player == null) { player = GetComponentInParent<Player>(); } }
        public void Interact() { player.Interact(); }
    }
}