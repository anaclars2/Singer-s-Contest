using UnityEngine;
using UISystem;
using AudioSystem;
using UnityEditor.Build.Profile;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int indexScene = 1;
    int totalScenesInBuild;

    public static GameManager instance;
    private void Awake() // singleton
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        totalScenesInBuild = SceneManager.sceneCountInBuildSettings;

        // UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
        // decidir ainda AudioManager.instance.PlaySfx();
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.instance.Animation(ANIMATION.SlideInAndOut, false);
        }
    }

    public void ChangeScene()
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
    }
}
