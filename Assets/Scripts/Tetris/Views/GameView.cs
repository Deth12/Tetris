using UnityEngine;
using Tetris.UI;
using Tetris.Controllers;
using Tetris.Extensions;
using TMPro;

namespace Tetris.Views
{
    public class GameView : BaseView
    {
        [SerializeField] private UI_Button _pauseButton = default;
        
        [SerializeField] private TMP_Text _levelCounter = default;
        [SerializeField] private TMP_Text _linesCounter = default;
        [SerializeField] private TMP_Text _scoreCounter = default;

        public override void Setup(GameController gameController)
        {
            base.Setup(gameController);
            
            _pauseButton.OnClick.AddListener(gameController.PauseGame);
        }
        
        public void UpdateLevelCounter(int value)
        {
            _levelCounter.text = value.ToString();
        }
    
        public void UpdateLinesCounter(int value)
        {
            _linesCounter.text = value.ToString();
        }
    
        public void UpdateScoreCounter(int value)
        {
            _scoreCounter.text = value.ToFormatedScore(9, '0');
        }
    }
}

