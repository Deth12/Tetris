using Tetris.Controllers;
using Tetris.UI;
using UnityEngine;

namespace Tetris.Views
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        [SerializeField] protected UI_Screen _screen;

        public virtual void Setup(GameController gameController)
        {
            _screen.SetupScreen();
        }

        public virtual void Show()
        {
            _screen.Show();
        }

        public virtual void Hide()
        {
            _screen.Hide();
        }
    }
}

