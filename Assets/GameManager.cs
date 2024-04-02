using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameInstance; //to expose this instance of game manager to grab
    private GameState State;
    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        GameInstance = this;
    }

    private void Start()
    {
        SetGameState(GameState.IDLE);
    }
    public void SetGameState(GameState newState)
    {
        State = newState;
        switch (State)
        {
            case GameState.OVER:
                HandleGameOver();
                break;
            case GameState.PLAYING:
                break;
            case GameState.IDLE:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void HandleGameOver()
    {
        Debug.Log("GameOver");
    }
}

public enum GameState
{
    IDLE, OVER, PLAYING
}
