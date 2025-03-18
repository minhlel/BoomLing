using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI textMeshPro;
    [SerializeField] public GameObject deathCanvas;
    public void PlayGame()
    {
        SceneManager.LoadScene("IntroScene"); 
    }

    public void quitGameButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void quizToMenuButton()
    {
        deathCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
   private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
             SceneManager.LoadScene(5);
        }
    }
}