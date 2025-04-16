using System.Collections;
using UnityEngine;

namespace UISystem
{
    public abstract class UIAnimation : MonoBehaviour
    {
        public ANIMATION type;

        public abstract IEnumerator AnimateAnimationIn();
        public abstract IEnumerator AnimateAnimationOut();
    }
}