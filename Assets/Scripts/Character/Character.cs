using UnityEngine;

namespace CharacterSystem
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Move Settings")]
        [SerializeField] protected float moveSpeed = 5f;
        protected Vector3 moveDirection;
        [SerializeField] protected SpriteRenderer sprite;

        public abstract void CharacterMove();
    }
}
