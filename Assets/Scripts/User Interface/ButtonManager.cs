using AudioSystem;
using UISystem;
using UnityEngine;
using SaveSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Enable/Disable Panel Settings")]
    [SerializeField] GameObject panel;

    [Header("Change Scene Settings")]
    [SerializeField] bool withTransition;
    [SerializeField] SCENES sceneToLoad = SCENES.None;
    [SerializeField] TRANSITION transition;

    [Header("Sound Settings")]
    [SerializeField] SOUND soundSelected;
    [SerializeField] SOUND soundHover;

    public void OpenPanel() { panel.SetActive(true); }

    public void ClosePanel() { panel.SetActive(false); }

    public void QuitGame() { Debug.Log("Leave Game"); Application.Quit(); }

    public void SoundSelected() { AudioManager.instance.PlaySfx(soundSelected); }

    public void SoundHover() { AudioManager.instance.PlaySfx(soundHover); }

    public void ChangeScene()
    {
        GameManager.instance.sceneToLoad = sceneToLoad;

        if (withTransition == false) { GameManager.instance.LoadScene(); }
        else { GameManager.instance.LoadSceneWithTransition(transition); }
    }

    // public void StartNewGame() { DataPersistenceManager.instance.NewGame(); }

    public void LoadGameProgress() { DataPersistenceManager.instance.LoadGame(); }

    public void SaveGameProgress() { DataPersistenceManager.instance.SaveGame(); }
}
