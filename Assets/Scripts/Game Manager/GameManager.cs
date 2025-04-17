using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int indexScene = 1;
    int totalScenesInBuild;
    public int levelToLoad;

    public static GameManager instance;
    private void Awake() // singleton
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        totalScenesInBuild = SceneManager.sceneCountInBuildSettings;

        // UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
        // decidir ainda AudioManager.instance.PlaySfx();
    }

    public void StartGame()
    {
        Debug.Log("Carregando cena..");
        SceneManager.LoadScene(levelToLoad);
    }

    /*  public void ChangeScene()
      {
          if (indexScene < totalScenesInBuild)
          {
              UIManager.instance.Transition(TRANSITION.CloseAndOpen, (SCENES)indexScene);
              indexScene++;
          }
          else
          {
              indexScene = 0;
              UIManager.instance.Transition(TRANSITION.CloseAndOpen, (SCENES)indexScene);
          }
      }*/
}
