using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

namespace UserInterface
{
    public class InterfaceManager : MonoBehaviour
    {
        public static InterfaceManager instance;

        [SerializeField] GameObject transitionContainer;
        [SerializeField] Slider progressBar; // mostrar % de carregamento da cena
        [SerializeField] SceneTransition[] transitions; // apenas para ver

        private void Awake() // singleton
        {
            if (instance == null) { instance = this; }
            else { Destroy(gameObject); }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            transitions = transitionContainer.GetComponentsInChildren<SceneTransition>();
        }

        #region Transitions
        public void DoTransition(TRANSITION transitionType, SCENES scene = SCENES.None)
        {
            StartCoroutine(Transition(transitionType, scene));
        }

        private IEnumerator Transition(TRANSITION transitionType, SCENES scene = SCENES.None)
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
            else
            {
                yield return new WaitForSeconds(0.7f); ;
            }
            yield return transition.AnimateTransitionOut();
        }
        #endregion
    }
}