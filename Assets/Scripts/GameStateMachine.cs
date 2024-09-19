using System;
using Input;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private InputReaderSwitcher _inputReaderSwitcher;

    private GameState _currentState;
    
    public GameState CurrentState => _currentState;
    public static GameStateMachine Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.Gameplay);
    }

    public void ChangeState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Gameplay:
                GameplayState();
                break;
            case GameState.Menu:
                MenuState();
                break;
        }
    }

    private void GameplayState()
    {
        _inputReaderSwitcher.SetActiveInputHandler(InputHandlerType.Gameplay);
        _currentState = GameState.Gameplay;
    }
    
    private void MenuState()
    {
        _inputReaderSwitcher.SetActiveInputHandler(InputHandlerType.UI);
    }
}