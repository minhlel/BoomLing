using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // public GameObject settingsPanel;

    // public void PlayGame()
    // {
    //     SceneManager.LoadScene("GameScene");
    // }

    // public void OpenSettings()
    // {
    //     settingsPanel.SetActive(true);
    // }

    // public void CloseSettings()
    // {
    //     settingsPanel.SetActive(false);
    // }

    public void QuitGame()
    {
        Application.Quit();
    }
}