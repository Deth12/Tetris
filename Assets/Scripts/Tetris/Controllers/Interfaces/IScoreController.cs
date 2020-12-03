using System;
using Zenject;

namespace Tetris.Controllers
{
    public interface IScoreController : IInitializable, IDisposable
    {
        void CollectPlacedBlock();
        void CollectClearedLines(int linesAmount);
        (int level, int lines, int score) GetResults();
    }
}

