using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTime = 2f;

    private void Update()
    {
        CheckSpacePressed();
    }

    private void CheckSpacePressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadNextLevel(LevelLoadDelayTime));
        }
    }

    IEnumerator LoadNextLevel(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(1);
        }
    }
}

