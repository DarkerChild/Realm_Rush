using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    public void LoadGameEasy()
    {
        gameController.currentDifficulty = GameController.difficulty.Easy;
        StartCoroutine(GoToGame());
    }
    public void LoadGameMedium()
    {
        gameController.currentDifficulty = GameController.difficulty.Medium;
        StartCoroutine(GoToGame());
    }
    public void LoadGameHard()
    {
        gameController.currentDifficulty = GameController.difficulty.Hard;
        StartCoroutine(GoToGame());
    }

    IEnumerator GoToGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
        }
    }
}
