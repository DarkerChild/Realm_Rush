using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Text date1 = null;
    [SerializeField] Text date2 = null;
    [SerializeField] Text date3 = null;
    [SerializeField] Text date4 = null;
    [SerializeField] Text date5 = null;
    [SerializeField] Text dateFinal = null;
    [SerializeField] Text score1 = null;
    [SerializeField] Text score2 = null;
    [SerializeField] Text score3 = null;
    [SerializeField] Text score4 = null;
    [SerializeField] Text score5 = null;
    [SerializeField] Text scoreFinal = null;

    SaveScript saveScript;

    void Start()
    {
        saveScript = FindObjectOfType<SaveScript>();
        UpdateGameOverScreen();
    }

    private void UpdateGameOverScreen()
    {
        date1.text = saveScript.highScoreDate1;
        date2.text = saveScript.highScoreDate2;
        date3.text = saveScript.highScoreDate3;
        date4.text = saveScript.highScoreDate4;
        date5.text = saveScript.highScoreDate5;
        dateFinal.text = saveScript.finalDate;
        score1.text = saveScript.highScore1.ToString();
        score2.text = saveScript.highScore2.ToString();
        score3.text = saveScript.highScore3.ToString();
        score4.text = saveScript.highScore4.ToString();
        score5.text = saveScript.highScore5.ToString();
        scoreFinal.text = saveScript.finalScore.ToString();
    }
}
