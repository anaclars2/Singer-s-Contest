using UnityEngine;
using UnityEngine.Windows;
using System;

namespace RhythmSystem
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] Lane lane;
        [Header("Visual Settings")]
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite defaultImage;
        [SerializeField] Sprite pressedImage;

        private void Start()
        {
            if (lane == null) { lane = GetComponentInParent<Lane>(); }
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(lane.input)) { spriteRenderer.sprite = pressedImage; }
            if (UnityEngine.Input.GetKeyUp(lane.input)) { spriteRenderer.sprite = defaultImage; }
        }
    }
}