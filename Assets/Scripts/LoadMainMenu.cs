using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        StartCoroutine(GoToStart());
    }

    IEnumerator GoToStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }
    }
}
