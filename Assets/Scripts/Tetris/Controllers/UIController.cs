using UnityEngine;
using Tetris.StateMachines;
using Tetris.UI;
using Tetris.Views;
using TMPro;

namespace Tetris.Controllers
{
    public class UIController : MonoBehaviour
    {
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
            _gameView.Setup(_gameController);
            _pauseView.Setup(_gameController);
            _endgameView.Setup(_gameController);
            
            SubscribeViews();
        }

        private void SubscribeViews()
        {
            _gameController.OnGamePause += _pauseView.Show;
            _gameController.OnGameUnpause += _pauseView.Hide;
            _gameController.OnGameEnd += _endgameView.Show;
        }
    }
}
