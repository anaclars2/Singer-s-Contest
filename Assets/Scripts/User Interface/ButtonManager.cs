using AudioSystem;
using UISystem;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [Header("Enable/Disable Panel Settings")]
    [SerializeField] GameObject panel;

    [Header("Change Scene Settings")]
    [SerializeField] bool withTransition;
    [SerializeField] SCENES sceneToLoad = SCENES.None;
    [SerializeField] TRANSITION transition;

    [Header("Sound Settings")]
    [SerializeField] SOUND sound;

    public void OpenPanel() { panel.SetActive(true); }

    public void ClosePanel() { panel.SetActive(false); }

    public void QuitGame() { Application.Quit(); Debug.Log("Leave Game"); }

    public void SoundClick() { AudioManager.instance.PlaySfx(sound); }

    public void ChangeScene()
    {
        GameManager.instance.sceneToLoad = sceneToLoad;

        if (withTransition == false) { GameManager.instance.LoadScene(); }
        else { GameManager.instance.LoadSceneWithTransition(transition); }
    }

}
