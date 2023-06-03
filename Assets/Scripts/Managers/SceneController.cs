using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int allScenes;


    private void Start()
    {
        allScenes = SceneManager.sceneCountInBuildSettings;

    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


    public void LoadNextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) == allScenes)
        {
            LoadFirstLevel();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

   
}
