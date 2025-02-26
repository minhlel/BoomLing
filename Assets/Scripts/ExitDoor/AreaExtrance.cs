using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start()
    {
        if (SceneManagement.Instance == null)
        {
            Debug.LogError("SceneManagement.Instance đang bị null trong AreaEntrance!");
            return;
        }

        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.transform.position = this.transform.position;
                CameraController.Instance.SetPlayerCameraFollow();
            }
            else
            {
                Debug.LogError("PlayerController.Instance đang bị null trong AreaEntrance!");
            }
        }
    }
}
