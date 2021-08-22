using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Player.onPlayerDeath += GameOver;
    }

    public void WinGame()
    {
        IsGameOver = true;
        GameController.Instance.OpenWinMenu();
    }

    public void GameOver()
    {
        IsGameOver = true;
        GameController.Instance.OpenGameOverMenu();
    }
}
