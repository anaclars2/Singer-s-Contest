using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace UISystem
{
    public class CrossFade : SceneTransition
    {
        [SerializeField] CanvasGroup crossFade;

        private void Start()
        {
            if ((int)type == 0) { type = (TRANSITION)System.Enum.Parse(typeof(TRANSITION), this.name); }
        }

        public override IEnumerator AnimateTransitionIn()
        {
            var tweener = crossFade.DOFade(1f, 1f);
            yield return tweener.WaitForCompletion();
        }

        public override IEnumerator AnimateTransitionOut()
        {
            var tweener = crossFade.DOFade(0f, 1f);
            yield return tweener.WaitForCompletion();
        }
    }
}