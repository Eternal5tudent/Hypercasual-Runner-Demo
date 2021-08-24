using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameOver { get; private set; }
    public bool HasGameStarted { get; private set; }
    public static Action onGameStart;
    public static Action onGameWon;
    protected override void Awake()
    {
        base.Awake();
        Player.onPlayerDeath += GameOver;
    }

    private void Update()
    {
        if (!HasGameStarted)
        {
            if (InputManager.Instance.DragX != 0)
            {
                GameStarted();
            }
        }
    }

    public void WinGame()
    {
        IsGameOver = true;
        onGameWon?.Invoke();
        GameController.Instance.OpenWinMenu();
    }

    public void GameOver()
    {
        IsGameOver = true;
        GameController.Instance.OpenGameOverMenu();
    }

    public void GameStarted()
    {
        HasGameStarted = true;
        onGameStart?.Invoke();
        GameController.Instance.EnableTutorial(false);
    }
}
