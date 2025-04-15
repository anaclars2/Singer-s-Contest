using UnityEngine;

namespace RhythmSystem
{
    public class LongNoteEffect : MonoBehaviour
    {
        public float noteSpeed = 0;
        public Note note;

        private void Update()
        {
            if (noteSpeed != 0) { SpriteEndMove(); }
        }

        private void SpriteEndMove()
        {
            transform.rotation = Quaternion.identity;
            transform.position = transform.position + new Vector3(0, -noteSpeed * Time.deltaTime, 0);

            if (transform.position.y <= -3 || note == null) { Destroy(gameObject); }
        }

    }
}