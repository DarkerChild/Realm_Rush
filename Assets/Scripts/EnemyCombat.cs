using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] GameObject hitFX = null;
    [SerializeField] GameObject deathFX = null;

    [SerializeField] float shotsPerSecond = 1f;
    [SerializeField] int pointsValue = 30;

    float maxHealth;
    float currentHealth;
    float damagePerShot;

    LevelStats playerStats;
    Slider healthSlider;
    

    void Start()
    {
        playerStats = FindObjectOfType<LevelStats>();
        healthSlider = GetComponentInChildren<Slider>();
        UpdateHealthBar();
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
        FXPosition = FXPosition.normalized * 5 + transform.position + new Vector3(0f,4f,0f);
        GameObject hitFX = Instantiate(this.hitFX, FXPosition, Quaternion.identity);
        var FXduration = hitFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(hitFX, FXduration);
    }

    private void TakeDamage(GameObject other)
    {
        TowerGun towerGun = other.transform.parent.parent.GetComponent<TowerGun>();
        currentHealth -= towerGun.GetDamage();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / (float)maxHealth;
    }

    private void DestroyIfDead()
    {
        if (currentHealth <= 0)
        {
            UpdateScore();
            Destroy(gameObject);
        }
    }

    private void UpdateScore()
    {
        playerStats.AddScore(pointsValue);
    }

    public void StartDamagingEnemyBase()
    {
        StartCoroutine(DamageEnemyBase());
    }

    IEnumerator DamageEnemyBase()
    {
        FriendlyBase friendlyBase = FindObjectOfType<FriendlyBase>();
        while (friendlyBase.GetCurrentHealth() > 0)
        {
            friendlyBase.DamageFriendlyBase(damagePerShot);
            yield return new WaitForSeconds(1f / shotsPerSecond);
        }
    }

    public void SetEnemyVariables(float newMaxHealth, float newDamagePerShot)
    {
        currentHealth = newMaxHealth;
        maxHealth = newMaxHealth;
        damagePerShot = newDamagePerShot;
    }



    public IEnumerator DeathSequence(int objectNumber)
    {
        Text deathTimerText = GetComponentInChildren<Text>();
        yield return new WaitForSeconds(objectNumber / 10f);
        deathTimerText.text = "3";
        deathTimerText.enabled = true;
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "2";
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "1";
        yield return new WaitForSeconds(1f);
        deathTimerText.text = "0";
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject deathFX = Instantiate(this.deathFX, transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity);
        var FXduration = deathFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(deathFX, FXduration);
    }
}
