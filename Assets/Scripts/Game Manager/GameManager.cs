using UnityEngine;
using UnityEngine.SceneManagement;
using UISystem;
using SaveSystem;
using System.Collections.Generic;
using InventorySystem;

public class GameManager : MonoBehaviour, IDataPersistence
{
    // static int indexScene = 1;
    // int totalScenesInBuild;
    public SCENES sceneToLoad;
    public List<bool> rhythmVictory; // marca quais fases de combate o jogador ja passou
    public Dictionary<EVIDENCES, bool> currentsItem; // true = tem 

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
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == (int)SCENES.Exploration)
        {
            UIManager.instance.PauseSettings();
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
        UIManager.instance.Animation(ANIMATION.SlideInAndOut, false);
        UIManager.instance.Transition(transition, sceneToLoad);
        DataPersistenceManager.instance.LoadGame();
    }

    public void LoadData(GameData data) { rhythmVictory = data.rhythmVictory; }

    public void SaveData(GameData data) { data.rhythmVictory = rhythmVictory; }

    public void RhythmCombatVictory() { rhythmVictory.Add(true); }

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