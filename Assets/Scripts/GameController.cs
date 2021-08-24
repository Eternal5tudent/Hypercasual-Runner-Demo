using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject tutorial;

    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void OpenWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        int currentBuildNum = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildNum == lastSceneIndex)
        {
            SceneManager.LoadSceneAsync(0);
        }
        else
        {
            SceneManager.LoadSceneAsync(lastSceneIndex);
        }
    }

    public void EnableTutorial(bool enable)
    {
        tutorial.SetActive(enable);
    }
}
