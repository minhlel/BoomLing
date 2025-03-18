using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public void LoadIntroScene()
    {
        Debug.Log("Loading IntroScene from Main Menu");
        SceneManager.LoadScene("IntroScene");
    }
}