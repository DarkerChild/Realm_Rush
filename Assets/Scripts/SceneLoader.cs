using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTime = 1f;

    public void LoadScene(int scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    IEnumerator LoadSceneCoroutine(int scene)
    {
        while (true)
        {
            yield return new WaitForSeconds(LevelLoadDelayTime);
            SceneManager.LoadScene(scene);
        }
    }
}

