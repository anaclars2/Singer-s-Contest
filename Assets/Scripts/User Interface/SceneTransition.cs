using System.Collections;
using UnityEngine;
using UserInterface;

namespace UserInterface
{
    public abstract class SceneTransition : MonoBehaviour
    {
        public TRANSITION type;

        public abstract IEnumerator AnimateTransitionIn();
        public abstract IEnumerator AnimateTransitionOut();
    }
}