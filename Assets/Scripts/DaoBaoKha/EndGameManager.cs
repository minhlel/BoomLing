using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một instance
        }
    }

    private void Start()
    {
        // Chọn ngẫu nhiên một enemy và đặt làm boss
        SelectRandomBoss();
    }

    private void SelectRandomBoss()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Boss");
        if (enemies.Length > 0)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject boss = enemies[randomIndex];
            GangsterBossHealth bossHealth = boss.GetComponent<GangsterBossHealth>();
            if (bossHealth == null)
            {
                bossHealth = boss.AddComponent<GangsterBossHealth>();
            }
            boss.tag = "Boss"; // Đảm bảo tag đúng
            boss.GetComponent<SpriteRenderer>().color = Color.red; // Làm nổi bật boss
            boss.transform.localScale *= 1.5f; // Tăng kích thước
            Debug.Log($"Đã chọn boss: {boss.name}");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy enemy nào với tag 'Boss'!");
        }
    }

    public void BossKilled()
    {
        EndGame();
    }

    private void EndGame()
    {
        // Hủy tất cả GameObject trong scene hiện tại (trừ EndGameManager)
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj != gameObject) // Không hủy chính EndGameManager
            {
                Destroy(obj);
            }
        }

        // Chuyển sang scene end game
        SceneManager.LoadScene("EndGame");

        // Tự hủy EndGameManager sau khi chuyển scene
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("NguyenHP"); // Tải lại scene gameplay
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); // Quay về Main Menu (index 0)
    }
}