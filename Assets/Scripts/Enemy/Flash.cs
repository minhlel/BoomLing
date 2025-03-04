using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;


    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        if (playerHealth != null)
        {
            playerHealth.DetectDeath();
        }
        else
        {
            enemyHealth.DetectDeath();
        }
    }

}
