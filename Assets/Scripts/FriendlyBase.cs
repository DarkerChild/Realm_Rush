﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyBase : MonoBehaviour
{
    [SerializeField] float baseStartingHealth = 1000f;
    [SerializeField] GameObject deathFXtemplate = null;

    Slider healthSlider;
    float baseCurrentHealth;

    public PlayerStats playerStats;
    public SceneLoader sceneLoader;
    public SaveScript saveScript;

    bool isDead = false;

    private void Start()
    {
        baseCurrentHealth = baseStartingHealth;
        healthSlider = GetComponentInChildren<Slider>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        playerStats = FindObjectOfType<PlayerStats>();
        saveScript = FindObjectOfType<SaveScript>();
    }
    public void DamageFriendlyBase(float damage)
    {
        baseCurrentHealth -= damage;
        UpdateHealthBar();
        if (baseCurrentHealth<=0)
        {
            StartCoroutine(LevelLost());
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = baseCurrentHealth / baseStartingHealth;
    }


    IEnumerator LevelLost()
    {
        if (!isDead)
        {
            isDead = true;
            saveScript.SetFinalScore(playerStats.currentPoints);
            Time.timeScale = 1f;
            DestroyBase();

            Tower[] allTowers = FindObjectsOfType<Tower>();
            EnemyCombat[] allEnemies = FindObjectsOfType<EnemyCombat>();

            DestroyAllTowersAndenemies(allTowers, allEnemies);

            while (allTowers.Length != 0 || allEnemies.Length != 0)
            {
                allTowers = FindObjectsOfType<Tower>();
                allEnemies = FindObjectsOfType<EnemyCombat>();
                yield return 0;
            }

            yield return new WaitForSeconds(1f);
            sceneLoader.LoadScene(2);
        }
    }

    private void DestroyBase()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.Find("Canvas").gameObject.SetActive(false);
        GameObject deathFX = Instantiate(deathFXtemplate, transform.position + Vector3.up * 15f, Quaternion.identity);
        var FXduration = deathFX.GetComponent<ParticleSystem>().main.startLifetime.constantMax;
        Destroy(deathFX, FXduration);
    }

    private void DestroyAllTowersAndenemies(Tower[] allTowers, EnemyCombat[] allEnemies)
    {
        int objectNumber = 0;
        FindObjectOfType<EnemySpawner>().StopAllCoroutines(); //Stop spawning new enemies

        Array.Reverse(allTowers);
        foreach (Tower tower in allTowers)
        {
            //tower.KillTower(objectNumber);
            StartCoroutine(tower.DeathSequence(objectNumber));
            objectNumber += 1;
        }
        Array.Reverse(allEnemies);
        foreach (EnemyCombat enemy in allEnemies)
        {
            enemy.StopAllCoroutines();
            StartCoroutine(enemy.DeathSequence(objectNumber));
            enemy.GetComponent<EnemyMovement>().enabled = false;
            objectNumber += 1;
        }
    }

    public float GetCurrentHealth()
    {
        return baseCurrentHealth;
    }
}
