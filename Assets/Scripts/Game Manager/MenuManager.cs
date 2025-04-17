using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [Header("Menu Settings")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject optionsPanel;

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Leave Game");
        Application.Quit();
    }

}
