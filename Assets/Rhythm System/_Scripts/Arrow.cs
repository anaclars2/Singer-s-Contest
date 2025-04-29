using UISystem;
using UnityEngine;

namespace RhythmSystem
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] Lane lane;
        [Header("Visual Settings")]
        [SerializeField] SpriteRenderer spriteRenderer;
        public Sprite image;

        private void Start()
        {
            if (lane == null) { lane = GetComponentInParent<Lane>(); }
            if (spriteRenderer == null) { spriteRenderer = GetComponent<SpriteRenderer>(); }
            spriteRenderer.sprite = image;
        }

        private void Update()
        {
            if (spriteRenderer.sprite == null) { spriteRenderer.sprite = image; }
            if (UnityEngine.Input.GetKeyDown(lane.input)) { spriteRenderer.color = Color.gray; }
            if (UnityEngine.Input.GetKeyUp(lane.input)) { spriteRenderer.color = Color.white; }
        }
    }
}