using UnityEngine;
using UnityEngine.SceneManagement;
using UISystem;
using SaveSystem;

public class GameManager : MonoBehaviour
{
    // static int indexScene = 1;
    // int totalScenesInBuild;
    public SCENES sceneToLoad;

    public static GameManager instance;

    private void Awake() // singleton
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES.Menu)
        {
            UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
            // AudioManager.instance.PlaySfx();
        }
    }

    public void LoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene((int)sceneToLoad);
        DataPersistenceManager.instance.LoadGame();
    }

    public void LoadSceneWithTransition(TRANSITION transition = TRANSITION.CloseAndOpen)
    {
        DataPersistenceManager.instance.SaveGame();
        UIManager.instance.Transition(transition, sceneToLoad);
        DataPersistenceManager.instance.LoadGame();
    }

    #region AutoChangeScene
    // START
    // totalScenesInBuild = SceneManager.sceneCountInBuildSettings;

    // private void Update() { indexScene = SceneManager.GetActiveScene().buildIndex + 1; }

    /* public void ChangeScene(TRANSITION transition = TRANSITION.CloseAndOpen)
     {
         if (indexScene < totalScenesInBuild)
         {
             UIManager.instance.Transition(transition, (SCENES)indexScene);
         }
         else
         {
             indexScene = 0;
             UIManager.instance.Transition(transition, (SCENES)indexScene);
         }
     }*/
    #endregion
}
