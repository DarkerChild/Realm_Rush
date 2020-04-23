using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] GameObject hitFX = null;
    [SerializeField] GameObject deathFX = null;

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
        DestroyIfDead();
    }
    private void PlayHitFX(GameObject other)
    {
        Vector3 FXPosition = other.transform.position - transform.position;
        FXPosition = FXPosition.normalized * 2 + transform.position;
        GameObject hitFX = Instantiate(this.hitFX, FXPosition, Quaternion.identity);
        var FXduration = hitFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(hitFX, FXduration);
    }

    private void TakeDamage(GameObject other)
    {
        TowerGun towerGun = other.transform.parent.parent.GetComponent<TowerGun>();
        currentHealth -= towerGun.GetDamage();
    }

    private void DestroyIfDead()
    {
        if (currentHealth <= 0)
        {
            GameObject deathFX = Instantiate(this.deathFX, transform.position, Quaternion.identity);
            var FXduration = deathFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
            //Destroy(deathFX, FXduration);
            Destroy(gameObject);
        }
    }
}
