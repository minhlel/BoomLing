using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (PlayerHealth.Instance != null)
            {
                SceneManagement.Instance.SetPlayerHealth(PlayerHealth.Instance.currentHealth);
            }
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
