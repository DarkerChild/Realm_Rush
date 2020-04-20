using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    public int currentHealth; //todo make private
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        TowerGun towergun = other.transform.parent.parent.GetComponent<TowerGun>();
        currentHealth -= towergun.GetDamage();
        if (currentHealth <= 0) Destroy(gameObject);
        print(currentHealth);
    }
}
