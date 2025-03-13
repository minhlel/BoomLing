
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TextMeshProUGUI textMeshPro;
    [SerializeField] public GameObject deathCanvas;
    public void playGameButton()
    {
        if(PlayerHealth.Instance != null){
        PlayerHealth.Instance.currentHealth  = PlayerHealth.Instance.maxHealth;
        }
        SceneManager.LoadScene(1);
    }

    public void quizGameButton()
    {
        Application.Quit();
    }

    public void quizToMenuButton()
    {
        deathCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
