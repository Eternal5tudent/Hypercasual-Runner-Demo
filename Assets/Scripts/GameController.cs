using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject winMenu;

    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void OpenWinMenu()
    {
        winMenu.SetActive(true);
    }
}
