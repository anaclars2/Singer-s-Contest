using System.Collections;
using UnityEngine;

namespace UISystem
{
    public abstract class SceneTransition : MonoBehaviour
    {
        public TRANSITION type;

        public abstract IEnumerator AnimateTransitionIn();
        public abstract IEnumerator AnimateTransitionOut();
    }
}