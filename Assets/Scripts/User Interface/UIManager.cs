using RhythmSystem;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Timeline.DirectorControlPlayable;

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

        [Header("Panels Settings")]
        public GameObject pausePanel;
        public GameObject menuPanel;
        public GameObject settingsPanel;
        public GameObject creditsPanel;
        [HideInInspector] public bool pauseActive = false;

        public static UIManager instance;

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);


            if (transitionContainer != null) { transitions = transitionContainer.GetComponentsInChildren<SceneTransition>(); }
            if (animationContainer != null) { animations = animationContainer.GetComponentsInChildren<UIAnimation>(); }

            if (pausePanel != null) { pausePanel.SetActive(false); }
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

        public void PauseSettings()
        {
            if (pausePanel.activeInHierarchy == false)
            {
                pausePanel.SetActive(true);
                pauseActive = true;
                Animation(ANIMATION.SlideInAndOut, true);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                pausePanel.SetActive(false);
                pauseActive = false;
                Animation(ANIMATION.SlideInAndOut, false);
            }
        }

        private void Update()
        {
            if (settingsPanel == null) { settingsPanel = GameObject.Find("SettingsPanel"); }
            if (creditsPanel == null) { creditsPanel = GameObject.Find("CreditsPanel"); }

            if (SceneManager.GetActiveScene().buildIndex == (int)SCENES.Menu)
            {
                if (settingsPanel != null)
                {
                    if (settingsPanel.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
                    {
                        settingsPanel.SetActive(false);
                        menuPanel.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                    }
                }

                if (creditsPanel != null)
                {
                    if (creditsPanel.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
                    {
                        creditsPanel.SetActive(false);
                        menuPanel.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                    }
                }
            }
        }
    }
}