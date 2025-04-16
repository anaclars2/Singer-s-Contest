using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class SlideInAndOut : UIAnimation
    {
        [SerializeField] Image image;

        private void Start()
        {
            if ((int)type == 0) { type = (ANIMATION)System.Enum.Parse(typeof(ANIMATION), this.name); }
        }

        public override IEnumerator AnimateAnimationIn()
        {
            Debug.Log("BB");
            image.rectTransform.anchoredPosition = new Vector2(0f, -image.rectTransform.rect.height);
            var tween = image.rectTransform.DOAnchorPosY(0, 1f);
            yield return tween.WaitForCompletion();
        }

        public override IEnumerator AnimateAnimationOut()
        {
            float yTarget = -image.rectTransform.rect.height;
            var tween = image.rectTransform.DOAnchorPosY(yTarget, 1f);
            yield return tween.WaitForCompletion();
        }
    }
}