using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Flash flash;
    private Animator myAnimator;

    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        flash = GetComponent<Flash>();
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
        Destroy(gameObject);
    }
    public void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
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
        canTakeDamage = true;
    }
    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
