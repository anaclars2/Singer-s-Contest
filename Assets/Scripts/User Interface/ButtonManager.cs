using AudioSystem;
using UISystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        GameManager.instance.sceneToLoad = sceneToLoad;

        if (withTransition == false) { GameManager.instance.LoadScene(); }
        else { GameManager.instance.LoadSceneWithTransition(transition); }
    }

    // public void StartNewGame() { DataPersistenceManager.instance.NewGame(); }

    public void LoadGameProgress() { DataPersistenceManager.instance.LoadGame(); }

    public void SaveGameProgress() { DataPersistenceManager.instance.SaveGame(); }

    public void OnSelect(BaseEventData eventData) { AudioManager.instance.PlaySfx(soundSelected); }

    private void SoundClick() { AudioManager.instance.PlaySfx(soundClick); }
}
