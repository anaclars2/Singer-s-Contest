using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class CloseAndOpen : SceneTransition
    {
        [SerializeField] Image imageBot;
        [SerializeField] Image imageTop;

        private void Start()
        {
            if ((int)type == 0) { type = (TRANSITION)System.Enum.Parse(typeof(TRANSITION), this.name); }
        }

        public override IEnumerator AnimateTransitionIn()
        {
            // animacao comecando fora da tela
            imageBot.rectTransform.anchoredPosition = new Vector2(0f, -imageBot.rectTransform.rect.height);
            imageTop.rectTransform.anchoredPosition = new Vector2(0f, imageTop.rectTransform.rect.height);

            // indo para o centro
            var botTween = imageBot.rectTransform.DOAnchorPosY(0f, 1f);
            var topTween = imageTop.rectTransform.DOAnchorPosY(0f, 1f);

            // esperando as duas animacoes terminarem
            yield return DOTween.Sequence()
                .Join(botTween)
                .Join(topTween)
                .WaitForCompletion();
        }

        public override IEnumerator AnimateTransitionOut()
        {
            // animacao para abrir, ou seja, voltar para fora da tela
            var botTween = imageBot.rectTransform.DOAnchorPosY(-imageBot.rectTransform.rect.height, 1f);
            var topTween = imageTop.rectTransform.DOAnchorPosY(imageTop.rectTransform.rect.height, 1f);

            yield return DOTween.Sequence()
                .Join(botTween)
                .Join(topTween)
                .WaitForCompletion();
        }
    }
}