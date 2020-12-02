using Tetris.Controllers;

namespace Tetris.Views
{
    public interface IView
    {
        void Setup(GameController gameController);
        void Show();
        void Hide();
    }
}

