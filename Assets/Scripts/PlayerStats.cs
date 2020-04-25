using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxTowers = 2;
    public int currTowers = 0;
    public int towerLevel = 1;
    
    [SerializeField] int score = 0;

    Text scoreText;
    Text towerNoText;
    Text towerLevelText;

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
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        towerNoText = GameObject.Find("No of towers").GetComponent<Text>();
        towerLevelText = GameObject.Find("Tower Level").GetComponent<Text>();
        towerController = FindObjectOfType<TowersController>();
    }

    private void UpdateScoreBoard()
    {
        scoreText.text = score.ToString();
        towerNoText.text = "Towers: " + currTowers.ToString() + " (" + maxTowers.ToString() + ")";
        towerLevelText.text = "Tower level: " + towerLevel.ToString();
    }
    private void SetMaxTowers()
    {
        towerController.UpdateTowerCount(maxTowers);
    }

    public void AddScore(int additionalScore)
    {
        print("Additional score recieved of " + additionalScore + "points.");
        score += additionalScore;
        UpdateScoreBoard();
    }

    public void BuyTower()
    {
        if (score >=100)
        {
            score -= 100;
            UpdateScoreBoard();
            maxTowers += 1;
            SetMaxTowers();
        }
    }

    public void UpgradeTowers()
    {
        if(score>200)
        {
            score -= 200;
            UpdateScoreBoard();
            towerController.UpgradeTower();
            towerLevel += 1;
        }
    }

    public void NewTowerCreated()
    {
        currTowers += 1;
        UpdateScoreBoard();
    }

    public IEnumerator SetGameOver()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);
        }
    }
}
