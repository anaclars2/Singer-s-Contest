using UnityEngine;
using UISystem;
using AudioSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private string levelToLoad;

    private void Start()
    {

        // decidir ainda AudioManager.instance.PlaySfx();
    }

    private void Update()
    {

    }

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        } else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    //private void Test()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        UIManager.instance.Animation(ANIMATION.SlideInAndOut, true);
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        UIManager.instance.Animation(ANIMATION.SlideInAndOut, false);
    //    }
    //}

    public void StartGame()
    {
        Debug.Log("Carregando cena..");
        SceneManager.LoadScene(levelToLoad);
    }
}
