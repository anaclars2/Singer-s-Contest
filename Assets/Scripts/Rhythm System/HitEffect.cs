using UnityEngine;

namespace RhythmSystem
{
    public class HitEffect : MonoBehaviour
    {
        static float lifeTime = 1f;

        void Update() { Destroy(gameObject, lifeTime); }
    }
}