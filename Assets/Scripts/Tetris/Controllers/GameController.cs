using System;
using System.Collections;
using System.Collections.Generic;
using Tetris.Managers;
using UnityEngine;
using Tetris.StateMachines;
using UnityEngine.SocialPlatforms.Impl;

namespace Tetris.Controllers
{
    public class GameController : MonoBehaviour
    {
        private static bool _isGameActive = true;
        public static bool IsGameActive => _isGameActive;

        [Header("Controllers")] 
        [SerializeField] private UIController _uiController = default;
        [SerializeField] private ScoreManager _scoreManager = default;

        public ScoreManager ScoreManager => _scoreManager;
        
        // State Machine
        private StateMachine _stateMachine;
        
        private EmptyState _emptyState;
        private GameplayState _gameplayState;
        private PausedState _pausedState;
    
        // Events
        public event Action OnGamePause;
        public event Action OnGameUnpause;
        public event Action OnGameEnd;
        public event Action<int, int, int> OnResultsConcluded;

        private void Start()
        {
            _isGameActive = true;
            
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

        public void GameOver()
        {
            var results = _scoreManager.GetResults();
            OnGameEnd?.Invoke();
            OnResultsConcluded?.Invoke(results.level, results.lines, results.score);
        }

        public void RestartGame()
        {
            ScenesManager.Instance.LoadGameScene();
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}

