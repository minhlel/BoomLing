using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorailDestroy : MonoBehaviour
{
    private GameObject player_temp;
    [SerializeField] private Transform player; // Transform của nhân vật
    [SerializeField] private Transform targetPoint; // Điểm bạn muốn kiểm tra
    [SerializeField] private GameObject guideCanvas; // Canvas hoặc UI hướng dẫn
    [SerializeField] private float detectionRange = 1f; // Khoảng cách cần phát hiện

    private void Update()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        float distance = Vector2.Distance(player.position, targetPoint.position);
        if (distance <= detectionRange)
        {
            guideCanvas.SetActive(true); // Hiển thị hướng dẫn
        }
        else
        {
            guideCanvas.SetActive(false); // Ẩn hướng dẫn
        }
    }

}