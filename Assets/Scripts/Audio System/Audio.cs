using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    [CreateAssetMenu(fileName = "New Audio", menuName = "Scriptable Objects/Audio")]
    public class Audio : ScriptableObject
    {
        public string midiLocation;
        public bool isMusic = false;

        public MUSIC musicID;
        public SOUND soundID;

        public List<AudioClip> clip;
        public bool isLoop;
        public bool hasPitchVariation;
        public float minPitch = 1f;
        public float maxPitch = 1f;
    }
}