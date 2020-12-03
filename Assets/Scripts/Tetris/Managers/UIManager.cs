using UnityEngine;
using Tetris.Views;
using Tetris.Controllers;
using Tetris.Data;
using Zenject;

namespace Tetris.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PrepareView _prepareView = default;
        [SerializeField] private GameView _gameView = default;
        [SerializeField] private PauseView _pauseView = default;
        [SerializeField] private EndgameView _endgameView = default;

        private GameController _gameController;
        private ScoreController _scoreController;
        
        private PlayerStats _playerStats;
        
        [Inject]
        public void Construct(ScoreController scoreController, PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _scoreController = scoreController;
            
            _scoreController.OnCheckLinesToNextLevel += _gameView.UpdateNextLevelLinesCounter;
            
            _playerStats.OnLevelChange += _gameView.UpdateLevelCounter;
            _playerStats.OnLinesChange += _gameView.UpdateLinesCounter;
            _playerStats.OnScoreChange += _gameView.UpdateScoreCounter;
        }
        
        public void Initialize(GameController gameController)
        {
            _gameController = gameController;
            
            SetupViews();
        }

        private void SetupViews()
        {
            _prepareView.Setup(_gameController);
            _gameView.Setup(_gameController);
            _pauseView.Setup(_gameController);
            _endgameView.Setup(_gameController);
            
            SubscribeViews();
        }

        private void SubscribeViews()
        {
            _gameController.OnGameStart += _prepareView.Hide;
            _gameController.OnGameStart += _gameView.Show;
            _gameController.OnGamePause += _pauseView.Show;
            _gameController.OnGameUnpause += _pauseView.Hide;
            _gameController.OnGameEnd += _endgameView.Show;
        }

        private void OnDestroy()
        {
            _scoreController.OnCheckLinesToNextLevel -= _gameView.UpdateNextLevelLinesCounter;
            _playerStats.OnLevelChange -= _gameView.UpdateLevelCounter;
            _playerStats.OnLinesChange -= _gameView.UpdateLinesCounter;
            _playerStats.OnScoreChange -= _gameView.UpdateScoreCounter;

            UnsubscribeViews();
        }

        private void UnsubscribeViews()
        {
            _gameController.OnGameStart -= _prepareView.Hide;
            _gameController.OnGameStart -= _gameView.Show;
            _gameController.OnGamePause -= _pauseView.Show;
            _gameController.OnGameUnpause -= _pauseView.Hide;
            _gameController.OnGameEnd -= _endgameView.Show;
        }
    }
}
