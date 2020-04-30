using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTime = 1f;

    int numGameControllers;

    public int finalScore = 0;
    public string finalDate = "01/01/2019";

    public int[] scores;
    public string[] dates;

    public enum difficulty { Easy , Medium, Hard};
    public difficulty currentDifficulty = difficulty.Easy;

    private void Awake()
    {
        int numGameControllers = FindObjectsOfType<GameController>().Length;        //if more than music player in scene the ndestroy ourselves
        if (numGameControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scores = new int[6];
        dates = new string[6];
        GetSaveData();
    }

    private void GetSaveData()
    {
        if (!PlayerPrefs.HasKey("highScore1"))
        {
            PlayerPrefs.SetInt("highScore1", 9);
            PlayerPrefs.SetInt("highScore2", 9);
            PlayerPrefs.SetInt("highScore3", 9);
            PlayerPrefs.SetInt("highScore4", 9);
            PlayerPrefs.SetInt("highScore5", 9);
            PlayerPrefs.SetString("highScoreDate1", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate2", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate3", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate4", "01/01/2001");
            PlayerPrefs.SetString("highScoreDate5", "01/01/2001");
        }
        scores[0] = PlayerPrefs.GetInt("highScore1");
        scores[1] = PlayerPrefs.GetInt("highScore2");
        scores[2] = PlayerPrefs.GetInt("highScore3");
        scores[3] = PlayerPrefs.GetInt("highScore4");
        scores[4] = PlayerPrefs.GetInt("highScore5");
        scores[5] = finalScore;
        dates[0] = PlayerPrefs.GetString("highScoreDate1");
        dates[1] = PlayerPrefs.GetString("highScoreDate2");
        dates[2] = PlayerPrefs.GetString("highScoreDate3");
        dates[3] = PlayerPrefs.GetString("highScoreDate4");
        dates[4] = PlayerPrefs.GetString("highScoreDate5");
        dates[5] = finalDate;
    }

    public void SetFinalScore(int gameFinalScore)
    {
        finalScore = gameFinalScore;
        finalDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        scores[5] = finalScore;
        dates[5] = finalDate;

        Array.Sort(scores, dates);
        Array.Reverse(scores);
        Array.Reverse(dates);

        PlayerPrefs.SetInt("highScore1", scores[0]);
        PlayerPrefs.SetInt("highScore2", scores[1]);
        PlayerPrefs.SetInt("highScore3", scores[2]);
        PlayerPrefs.SetInt("highScore4", scores[3]);
        PlayerPrefs.SetInt("highScore5", scores[4]);
        PlayerPrefs.SetString("highScoreDate1", dates[0]);
        PlayerPrefs.SetString("highScoreDate2", dates[1]);
        PlayerPrefs.SetString("highScoreDate3", dates[2]);
        PlayerPrefs.SetString("highScoreDate4", dates[3]);
        PlayerPrefs.SetString("highScoreDate5", dates[4]);
    }

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    IEnumerator LoadSceneCoroutine(int scene)
    {
        while (SceneManager.GetActiveScene().buildIndex != scene)
        {
            yield return new WaitForSeconds(LevelLoadDelayTime);
            SceneManager.LoadScene(scene);
        }
    }
}
