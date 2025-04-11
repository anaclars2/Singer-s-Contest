using UnityEngine;

namespace CharacterSystem
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Character Settings")]
        public Sprite sprite;

        [Header("Move Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        protected Vector3 moveDirection;

        public abstract void Move();
    }
}
