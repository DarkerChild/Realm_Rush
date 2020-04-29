using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] float LevelLoadDelayTime = 1f;

    public void LoadScene(int scene)
    {
         StartCoroutine(LoadGame(scene, LevelLoadDelayTime));
    }

    IEnumerator LoadGame(int scene, float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(scene);
        }
    }
}

