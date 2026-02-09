using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public event Action<GameState, GameState> OnGameStateChanged;

    public Component shippedComponent { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetState(GameState.Idle);
    }

    public void SetState(GameState newState, Component shippedComponent = null)
    {
        if (newState == CurrentState)
            return;

        if (shippedComponent != null)
            this.shippedComponent = shippedComponent;
        else
            this.shippedComponent = null;

        GameState previousState = CurrentState;
        CurrentState = newState;

        HandleStateChange(previousState, newState);
        Debug.Log($"Game state changed from {previousState} to {newState}");
        OnGameStateChanged?.Invoke(previousState, newState);
    }

    private void HandleStateChange(GameState from, GameState to)
    {
        switch (to)
        {
            case GameState.Crafting:
                break;

            case GameState.Shopping:
                break;

            case GameState.Combat:
                break;

            case GameState.Equiping:
                break;

            case GameState.Idle:
                break;
        }
    }
}
[Serializable]
public enum GameState
{
    Crafting,
    Shopping,
    Combat,
    Equiping,
    Idle
}