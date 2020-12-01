using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetris.StateMachine;

public class GameController : MonoBehaviour
{
    private static bool _isGameActive = true;
    public static bool IsGameActive => _isGameActive;
    
    [Header("Controllers")] 
    [SerializeField] private UIController _uiController = default;
    
    // State Machine
    private StateMachine _stateMachine;
    
    private EmptyState _emptyState;
    private GameplayState _gameplayState;
    private PausedState _pausedState;

    // Events
    public Action OnGamePause;
    public Action OnGameUnpause;
    
    private void Start()
    {
        SetupStateMachine();
        SetupControllers();
    }

    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();
            
        _emptyState = new EmptyState(_stateMachine, this);
        _pausedState = new PausedState(_stateMachine, this);
        _gameplayState = new GameplayState(_stateMachine, this);

        _pausedState.SetNextState(_gameplayState);
        _gameplayState.SetNextState(_pausedState);

        _stateMachine.Initialize(_gameplayState);
    }

    private void SetupControllers()
    {
        _uiController.Initialize(this);
    }

    private void Update()
    {
        _stateMachine.CurrentState.HandleInput();
    }

    public void ChangeState(State state)
    {
        _stateMachine.ChangeState(state);
    }

    public void PauseGame()
    {
        _isGameActive = false;
        OnGamePause?.Invoke();
    }

    public void UnpauseGame()
    {
        _isGameActive = true;
        OnGameUnpause?.Invoke();
    }
}
