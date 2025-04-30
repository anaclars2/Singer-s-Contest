using AudioSystem;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
<<<<<<< HEAD
using Unity.VisualScripting.InputSystem;
using UnityEngine.SceneManagement;
=======
>>>>>>> AnaCode

public class ButtonManager : MonoBehaviour, ISelectHandler
{
    [SerializeField] Button button;

    [Header("Change Scene Settings")]
    [SerializeField] bool withTransition;
    [SerializeField] SCENES sceneToLoad = SCENES.None;
    [SerializeField] TRANSITION transition;

    [Header("Sound Settings")]
    [SerializeField] SOUND soundSelected;
    [SerializeField] SOUND soundClick;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SoundClick);
    }

    public void QuitGame() { Debug.Log("Leave Game"); Application.Quit(); }

    public void ChangeScene()
    {
        Debug.Log("teste funcionou");
        if (withTransition) { GameManager.instance.LoadScene(); }
        else { SceneManager.LoadScene((int)sceneToLoad); }
    }

    // public void StartNewGame() { DataPersistenceManager.instance.NewGame(); }

    public void OnSelect(BaseEventData eventData) { AudioManager.instance.PlaySfx(soundSelected); }

    private void SoundClick() { AudioManager.instance.PlaySfx(soundClick); }
}
