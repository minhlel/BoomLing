using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duc_explosionenemy : duc_eneemy
{
    [SerializeField] private GameObject explosionPrefabs;

    private void CreateExplsion()
    {
        if (explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        }
    }
    protected override void Die()
    {
        CreateExplsion();
        base.Die();
    }
     private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Kiểm tra va chạm với enemy hoặc wall
        if (collider2D.gameObject.CompareTag("Player"))
        {
           CreateExplsion();
        }
    }
}
