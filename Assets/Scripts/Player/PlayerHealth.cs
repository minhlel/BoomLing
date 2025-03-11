using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] public int maxHealth = 3;
    [SerializeField] private float damageRecoveryTime = 1f;

    private ButtonManager buttonManager;
    private Slider healthSlider;
    public int currentHealth;
    //private bool canTakeDamage = true;
    private Flash flash;
    private Animator myAnimator;

    protected override void Awake()
    {
        base.Awake();
        if (FindObjectsOfType<ButtonManager>().Length > 1)
        {
            Destroy(gameObject); // Xóa bản sao dư thừa nếu đã có một ButtonManager tồn tại
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Đối tượng không bị hủy khi chuyển scene
        }
        myAnimator = GetComponent<Animator>();
        flash = GetComponent<Flash>();
        if (SceneManagement.Instance != null && SceneManagement.Instance.PlayerHealth > 0)
        {
            currentHealth = SceneManagement.Instance.PlayerHealth;
            UpdateHealthSlider();
        }
    }
    public void Update()
    {
        UpdateHealthSlider();
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            bool isDeath = CheckIfPlayerDeath();
            if (isDeath)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.deathPlayer);
                myAnimator.SetBool("isDeath", isDeath ? true : false);
            }
        }
    }
    public void DestroyPlaeyer()
    {
        buttonManager = FindObjectOfType<ButtonManager>();
        buttonManager.deathCanvas.gameObject.SetActive(true);
        buttonManager.textMeshPro.text = "Score:" + ScoreManager.Instance.DisplayScore();
        Destroy(gameObject);
    }
    public void TakeDamage(int damageAmount)
    {
        //canTakeDamage = false;
        currentHealth -= damageAmount;
        Debug.Log(currentHealth);
        UpdateHealthSlider();
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(DamageRecoveryRoutine());
    }
    public Boolean CheckIfPlayerDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("Player Death");
            return true;
        }
        return false;
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        //canTakeDamage = true;
    }
    public void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
