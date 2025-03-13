using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private float socre = 10;
    //[SerializeField] private HealthBar healthBar;

    private int currentHealth;
    private Flash flash;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        flash = GetComponent<Flash>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        StartCoroutine(flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            ScoreManager.Instance.Score(socre);
            myAnimator.SetBool("Death", true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.deathEnemy);
        }
    }
    public void DestroyBot()
    {
        Destroy(gameObject);
    }

}
