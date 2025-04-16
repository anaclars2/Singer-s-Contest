using System.Collections;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

namespace UISystem
{
    public class UIManager : MonoBehaviour
    {
        [Header("Transitions Settings")]
        [SerializeField] GameObject transitionContainer;
        [SerializeField] Slider progressBar; // mostrar % de carregamento da cena
        SceneTransition[] transitions;

        [Header("Animations Settings")]
        [SerializeField] GameObject animationContainer;
        UIAnimation[] animations;

        public static UIManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (transitionContainer != null) { transitions = transitionContainer.GetComponentsInChildren<SceneTransition>(); }
            if (animationContainer != null) { animations = animationContainer.GetComponentsInChildren<UIAnimation>(); }
        }

        #region SceneTransition
        public void Transition(TRANSITION transitionType, SCENES scene = SCENES.None)
        {
            StartCoroutine(IEnumeratorTransition(transitionType, scene));
        }

        private IEnumerator IEnumeratorTransition(TRANSITION transitionType, SCENES scene = SCENES.None)
        {
            SceneTransition transition = transitions.First(t => t.type == transitionType);
            if (transition == null) { yield break; }

            yield return transition.AnimateTransitionIn();
            if (scene != SCENES.None)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)scene);
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
            }
            else { yield return new WaitForSeconds(0.7f); ; }
            yield return transition.AnimateTransitionOut();
        }
        #endregion

        #region Animation
        public void Animation(ANIMATION animationType, bool starting)
        {
            if (starting == true) { StartCoroutine(IEnumeratorAnimationIn(animationType)); }
            else { StartCoroutine(IEnumeratorAnimationOut(animationType)); }
        }

        private IEnumerator IEnumeratorAnimationIn(ANIMATION animationType)
        {
            UIAnimation animation = animations.First(a => a.type == animationType);
            if (animation == null) { yield break; }
            yield return animation.AnimateAnimationIn();
        }

        private IEnumerator IEnumeratorAnimationOut(ANIMATION animationType)
        {
            UIAnimation animation = animations.First(a => a.type == animationType);
            if (animation == null) { yield break; }

            yield return animation.AnimateAnimationOut();
        }
        #endregion
    }
}