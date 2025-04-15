using UnityEngine;

namespace RhythmSystem
{
    public class HitEffect : MonoBehaviour
    {
        static float lifeTime = 0.7f;

        void Update() { Destroy(gameObject, lifeTime); }
    }
}