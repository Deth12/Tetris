using System.Collections;
using System.Collections.Generic;
using Tetris.Controllers;
using UnityEngine;
using Tetris.UI;
using TMPro;

namespace Tetris.Views
{
    [RequireComponent(typeof(UI_Screen))]
    public class EndgameView : BaseView
    {
        [SerializeField] private UI_Button _restartButton = default;
        [SerializeField] private UI_Button _exitButton = default;
        
        [SerializeField] private TMP_Text _levelResult = default;
        [SerializeField] private TMP_Text _linesResult = default;
        [SerializeField] private TMP_Text _scoreResult = default;
        
        public override void Setup(GameController gameController)
        {
            base.Setup(gameController);
            
            _restartButton.OnClick.AddListener(gameController.RestartGame);
            _exitButton.OnClick.AddListener(gameController.ExitGame);

            gameController.OnResultsConcluded += FillResults;
        }

        public void FillResults(int levelResult, int linesResult, int scoreResult)
        {
            _levelResult.text = levelResult.ToString();
            _linesResult.text = linesResult.ToString();
            _scoreResult.text = scoreResult.ToString();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}
