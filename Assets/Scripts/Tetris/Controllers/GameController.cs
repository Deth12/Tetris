using System;
using Tetris.Constants;
using Tetris.Managers;
using Tetris.StateMachines;

namespace Tetris.Controllers
{
    public class GameController : IGameController
    {
        private static bool _isGameActive = true;
        public static bool IsGameActive => _isGameActive;

        private ScoreController _scoreController;
        private BlockController _blockController;
        private UIManager _uiManager;
        private SpawnManager _spawnManager;
        
        // State Machine
        private StateMachine _stateMachine;
        
        private PrepareState _prepareState;
        private GameplayState _gameplayState;
        private PausedState _pausedState;
    
        // Events
        public event Action OnGameStart;
        public event Action OnGamePause;
        public event Action OnGameUnpause;
        public event Action OnGameEnd;
        public event Action<int, int, int> OnResultsConcluded;

        public GameController(ScoreController scoreController, BlockController blockController, 
            UIManager uiManager, SpawnManager spawnManager)
        {
            _scoreController = scoreController;
            _blockController = blockController;
            _uiManager = uiManager;
            _spawnManager = spawnManager;
        }
        
        public void Initialize()
        {
            _blockController.OnBlockStuck += GameOver;
            _uiManager.Initialize(this);

            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();
                
            _prepareState = new PrepareState(_stateMachine, this);
            _pausedState = new PausedState(_stateMachine, this);
            _gameplayState = new GameplayState(_stateMachine, this);
    
            _prepareState.SetNextState(_gameplayState);
            _gameplayState.SetNextState(_pausedState);
            _pausedState.SetNextState(_gameplayState);
    
            _stateMachine.Initialize(_prepareState);
        }
    
        public void Tick()
        {
            _stateMachine.CurrentState.HandleInput();
        }

        public void ChangeState(State state)
        {
            _stateMachine.ChangeState(state);
        }

        public void StartGame()
        {
            _isGameActive = true;
            
            AudioManager.Instance.PlayClipByName(ConstantAudioNames.GAME_NEWGAME);
            
            _spawnManager.SpawnBlock();
            OnGameStart?.Invoke();
        }
        
        public void PauseGame()
        {
            _isGameActive = false;
         
            AudioManager.Instance.PlayClipByName(ConstantAudioNames.GAME_PAUSE);

            OnGamePause?.Invoke();
        }
    
        public void UnpauseGame()
        {
            _isGameActive = true;
         
            AudioManager.Instance.PlayClipByName(ConstantAudioNames.GAME_RESUME);

            OnGameUnpause?.Invoke();
        }

        public void GameOver()
        {
            var results = _scoreController.GetResults();
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

