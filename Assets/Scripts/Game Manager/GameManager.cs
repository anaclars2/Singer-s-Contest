using UnityEngine;
using UnityEngine.SceneManagement;
using UISystem;
using SaveSystem;
using RhythmSystem;
using AudioSystem;

public class GameManager : MonoBehaviour
{
    // static int indexScene = 1;
    // int totalScenesInBuild;
    public SCENES sceneToLoad;
    MUSIC musicID = 0;
    public Transition2 transitionEffect;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != (int)SCENES.Menu)
        {
            UIManager.instance.PauseSettings();
        }
    }

    public void LoadScene(SCENES scene)
    {
        sceneToLoad = scene;
        DataPersistenceManager.instance.SaveGame();
        transitionEffect.StartTransition(scene);

        //// passando qual musica e para tocar
        //if (SceneManager.GetActiveScene().buildIndex == (int)SCENES.Rythm)
        //{
        //    musicID++;
        //    RhythmManager.instance.musicID = musicID;
        //}
    }

    //public void LoadSceneWithTransition(TRANSITION transition = TRANSITION.CloseAndOpen)
    //{
    //    DataPersistenceManager.instance.SaveGame();
    //    //UIManager.instance.Animation(ANIMATION.SlideInAndOut, false);
    //    //UIManager.instance.Transition(transition, sceneToLoad);
    //    transitionEffect.StartTransition(sceneToLoad);
    //    DataPersistenceManager.instance.LoadGame();
    //}

    public void FinishSceneLoad(SCENES scene)
    {
        SceneManager.LoadScene((int)scene);
        DataPersistenceManager.instance.LoadGame();

        if (scene == SCENES.Rythm) {
            musicID++;
            RhythmManager.instance.musicID = musicID;
        }
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