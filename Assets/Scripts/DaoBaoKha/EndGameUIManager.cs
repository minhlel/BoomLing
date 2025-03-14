using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUIManager : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("DaoBaoKha"); // Chuyển về scene gameplay
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Dừng Play Mode trong Editor
        #else
            Application.Quit(); // Thoát ứng dụng khi build
        #endif
    }
}