using System;
using Zenject;

namespace Tetris.Controllers
{
    public interface IScoreController : IInitializable, IDisposable
    {
        void AddScore(int value);
    }
}

