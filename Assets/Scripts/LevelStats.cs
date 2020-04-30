using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStats : MonoBehaviour
{
    public int maxTowers = 1;
    public int currTowers = 0;
    public int towerLevel = 1;
    
    public int currentPoints = 0;
    public int totalPoints = 0;

    public int towerBuyCost = 100;
    public int towerUpgradeCost = 200;

    [SerializeField] Text currentPointsText;
    [SerializeField] Text totalPointsText;
    [SerializeField] Text towerNoText;
    [SerializeField] Text towerLevelText;
    [SerializeField] Text towerDamage;
    [SerializeField] Text towerROF;
    [SerializeField] Text towerDPS;

    [SerializeField] Text buyTowerText;
    [SerializeField] Text upgradeTowerText;

    [SerializeField] GameObject buyTower;
    [SerializeField] GameObject towerUpgrade;

    [SerializeField] TowersController towerController;
    GameController gameController;

    private void Start()
    {
        CheckUnique();
        gameController = FindObjectOfType<GameController>();
        SetDifficulty();
        
        UpdateUI();
        SetMaxTowers();
    }

    private void CheckUnique()
    {
        LevelStats[] playerStats = FindObjectsOfType<LevelStats>();
        if (playerStats.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void SetDifficulty()
    {
        switch (gameController.currentDifficulty)
        {
            case GameController.difficulty.Easy:
                towerBuyCost = 70;
                towerUpgradeCost = 200;
                break;
            case GameController.difficulty.Medium:
                towerBuyCost = 85;
                towerUpgradeCost = 300;
                break;
            case GameController.difficulty.Hard:
                towerBuyCost = 100;
                towerUpgradeCost = 400;
                break;
        }
    }

    private void UpdateUI()
    {
        currentPointsText.text = currentPoints.ToString();
        totalPointsText.text = totalPoints.ToString();
        towerNoText.text = "Towers: " + currTowers.ToString() + " (" + maxTowers.ToString() + ")";
        towerLevelText.text = "Tower level: " + towerLevel.ToString();
        towerDamage.text = "Damage : " + Mathf.FloorToInt(towerController.damagePerShot).ToString();
        towerROF.text = "ROF : " + Mathf.FloorToInt(towerController.shotsPerSecond).ToString();
        towerDPS.text = "DPS : " + Mathf.FloorToInt(towerController.damagePerShot* towerController.shotsPerSecond).ToString();
        buyTowerText.text = "Buy Tower (" + towerBuyCost + ")";
        upgradeTowerText.text = "Upgrade all Towers (" + towerUpgradeCost + ")";
        SetTowerButtonVisibility();
    }

    private void SetTowerButtonVisibility()
    {
        if (currentPoints < towerBuyCost)
        {
            buyTower.GetComponent<Button>().enabled = false;
            buyTower.GetComponent<CanvasGroup>().alpha = 0.4f;
        }
        else
        {
            buyTower.GetComponent<Button>().enabled = true;
            buyTower.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (currentPoints < towerUpgradeCost)
        {
            towerUpgrade.GetComponent<Button>().enabled = false;
            towerUpgrade.GetComponent<CanvasGroup>().alpha = 0.4f;
        }
        else
        {
            towerUpgrade.GetComponent<Button>().enabled = true;
            towerUpgrade.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private void SetMaxTowers()
    {
        towerController.UpdateTowerCount(maxTowers);
    }

    public void AddScore(int additionalScore)
    {
        currentPoints += additionalScore;
        totalPoints += additionalScore;
        UpdateUI();
    }

    public void BuyTower()
    {
        currentPoints -= 100;
        maxTowers += 1;
        UpdateUI();
        SetMaxTowers();
    }

    public void UpgradeTowers()
    {
        currentPoints -= 200;
        towerLevel += 1;
        UpdateUI();
        towerController.UpgradeTower();
    }

    public void NewTowerCreated()
    {
        currTowers += 1;
        UpdateUI();
    }
}
