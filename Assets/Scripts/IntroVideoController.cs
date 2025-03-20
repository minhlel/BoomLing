using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroVideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public Button skipButton; 

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        skipButton.onClick.AddListener(OnSkipButtonClicked); 
    }

    private void Start()
    {
        
        videoPlayer.Play();

        
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadGameplayScene();
    }

    private void OnSkipButtonClicked()
    {
        videoPlayer.Stop(); 
        LoadGameplayScene(); 
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene("LeKhaiMinh"); 
    }
}