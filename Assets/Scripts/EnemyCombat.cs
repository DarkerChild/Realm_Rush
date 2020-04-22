using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject hitFX = null;

    private int currentHealth; //todo make private
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        PlayHitFX(other);
        TakeDamage(other);
        
    }

    private void TakeDamage(GameObject other)
    {
        TowerGun towergun = other.transform.parent.parent.GetComponent<TowerGun>();
        currentHealth -= towergun.GetDamage();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayHitFX(GameObject other)
    {
        if (hitFX != null)
        {
            Vector3 FXPosition = other.transform.position - transform.position;
            FXPosition = FXPosition.normalized * 2 + transform.position;
            GameObject hitFX1 = Instantiate(hitFX, FXPosition, Quaternion.identity);
            Destroy(hitFX1, 1f);
        }
        else
        {
            Debug.Log("No Hit FX assigned");
        }
    }
}
