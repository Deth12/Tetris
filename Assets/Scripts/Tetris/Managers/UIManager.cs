using UnityEngine;
using Tetris.Views;
using Tetris.Controllers;

namespace Tetris.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PrepareView _prepareView = default;
        [SerializeField] private GameView _gameView = default;
        [SerializeField] private PauseView _pauseView = default;
        [SerializeField] private EndgameView _endgameView = default;

        public GameView GameView => _gameView;
        
        private GameController _gameController;
        
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
