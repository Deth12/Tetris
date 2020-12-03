using Tetris.Controllers;
using UnityEngine;
using Tetris.UI;

namespace Tetris.Views
{
    public class PauseView : BaseView
    {
        [SerializeField] private UI_Button _resumeButton = default;
        [SerializeField] private UI_Button _restartButton = default;

        public override void Setup(GameController gameController)
        {
            base.Setup(gameController);

            _resumeButton.OnClick.AddListener(gameController.UnpauseGame);
            _restartButton.OnClick.AddListener(gameController.RestartGame);
        }
    }
}

