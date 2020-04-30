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

    GameController gameController;

    bool isPopulated = false;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    void Update()
    {
        if (!isPopulated)
        {
            isPopulated = true;
            UpdateGameOverScreen();
        }
    }

    private void UpdateGameOverScreen()
    {
        date1.text = PlayerPrefs.GetString("highScoreDate1");
        date2.text = PlayerPrefs.GetString("highScoreDate2");
        date3.text = PlayerPrefs.GetString("highScoreDate3");
        date4.text = PlayerPrefs.GetString("highScoreDate4");
        date5.text = PlayerPrefs.GetString("highScoreDate5");
        dateFinal.text = gameController.finalDate;
        score1.text = PlayerPrefs.GetInt("highScore1").ToString();
        score2.text = PlayerPrefs.GetInt("highScore2").ToString();
        score3.text = PlayerPrefs.GetInt("highScore3").ToString();
        score4.text = PlayerPrefs.GetInt("highScore4").ToString();
        score5.text = PlayerPrefs.GetInt("highScore5").ToString();
        scoreFinal.text = gameController.finalScore.ToString();
    }
}
