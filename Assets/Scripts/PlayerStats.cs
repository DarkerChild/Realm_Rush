using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxTowers = 1;
    public int currTowers = 0;
    public int towerLevel = 1;
    
    public int currentPoints = 0;
    public int totalPoints = 0;

    [SerializeField] int towerBuyCost = 100;
    [SerializeField] int towerUpgradeCost = 200;

    Text currentPointsText;
    Text totalPointsText;
    Text towerNoText;
    Text towerLevelText;
    Text towerDamage;
    Text towerROF;
    Text towerDPS;

    GameObject buyTower;
    GameObject towerUpgrade;

    TowersController towerController;

    private void Start()
    {
        CheckUnique();
        GetRelatedObjects();
        UpdateScoreBoard();
        SetMaxTowers();
    }


    private void CheckUnique()
    {
        PlayerStats[] playerStats = FindObjectsOfType<PlayerStats>();
        if (playerStats.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void GetRelatedObjects()
    {
        currentPointsText = GameObject.Find("Current Points").GetComponent<Text>();
        totalPointsText = GameObject.Find("Total Points").GetComponent<Text>();
        towerNoText = GameObject.Find("No of towers").GetComponent<Text>();
        towerLevelText = GameObject.Find("Tower Level").GetComponent<Text>();
        towerDamage = GameObject.Find("Tower Damange").GetComponent<Text>();
        towerROF = GameObject.Find("Tower ROF").GetComponent<Text>();
        towerDPS = GameObject.Find("Tower DPS").GetComponent<Text>();
        towerController = FindObjectOfType<TowersController>();
        buyTower = GameObject.Find("Buy Tower Button");
        towerUpgrade = GameObject.Find("Upgrade Tower Button");

    }

    private void UpdateScoreBoard()
    {
        currentPointsText.text = currentPoints.ToString();
        totalPointsText.text = totalPoints.ToString();
        towerNoText.text = "Towers: " + currTowers.ToString() + " (" + maxTowers.ToString() + ")";
        towerLevelText.text = "Tower level: " + towerLevel.ToString();
        towerDamage.text = "Damage : " + Mathf.FloorToInt(towerController.damagePerShot).ToString();
        towerROF.text = "ROF : " + Mathf.FloorToInt(towerController.shotsPerSecond).ToString();
        towerDPS.text = "DPS : " + Mathf.FloorToInt(towerController.damagePerShot* towerController.shotsPerSecond).ToString();
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
        UpdateScoreBoard();
    }

    public void BuyTower()
    {
        currentPoints -= 100;
        maxTowers += 1;
        UpdateScoreBoard();
        SetMaxTowers();
    }

    public void UpgradeTowers()
    {
        currentPoints -= 200;
        towerLevel += 1;
        UpdateScoreBoard();
        towerController.UpgradeTower();
    }

    public void NewTowerCreated()
    {
        currTowers += 1;
        UpdateScoreBoard();
    }
}
