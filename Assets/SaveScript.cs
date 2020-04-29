using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScript : MonoBehaviour
{
    public int finalScore = 0;
    public string finalDate = "01/01/2019";

    public int highScore1;
    public int highScore2;
    public int highScore3;
    public int highScore4;
    public int highScore5;

    public string highScoreDate1;
    public string highScoreDate2;
    public string highScoreDate3;
    public string highScoreDate4;
    public string highScoreDate5;

    List<int> highScores;

    private void Start()
    {
        CheckUnique();
        highScores = new List<int>();
        GetSaveData();
    }

    private void GetSaveData()
    {
        if (!PlayerPrefs.HasKey("highScore1"))
        {
            PlayerPrefs.SetInt("highScore1", 999);
            PlayerPrefs.SetInt("highScore2", 999);
            PlayerPrefs.SetInt("highScore3", 999);
            PlayerPrefs.SetInt("highScore4", 999);
            PlayerPrefs.SetInt("highScore5", 999);
            PlayerPrefs.SetString("highScoreDate1", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate2", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate3", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate4", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate5", "01/01/2001");
        }
        highScore1 = PlayerPrefs.GetInt("highScore1");
        highScore2 = PlayerPrefs.GetInt("highScore2");
        highScore3 = PlayerPrefs.GetInt("highScore3");
        highScore4 = PlayerPrefs.GetInt("highScore4");
        highScore5 = PlayerPrefs.GetInt("highScore5");
        highScoreDate1 = PlayerPrefs.GetString("highScoreDate1");
        highScoreDate2 = PlayerPrefs.GetString("highScoreDate2");
        highScoreDate3 = PlayerPrefs.GetString("highScoreDate3");
        highScoreDate4 = PlayerPrefs.GetString("highScoreDate4");
        highScoreDate5 = PlayerPrefs.GetString("highScoreDate5");
    }

    private void CheckUnique()
    {
        SaveScript[] saveScripts = FindObjectsOfType<SaveScript>();
        if (saveScripts.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetFinalScore(int gameFinalScore)
    {
        finalScore = gameFinalScore;
    }
}
