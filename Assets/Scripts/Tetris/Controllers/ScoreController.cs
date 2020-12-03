using Tetris.Configs;

namespace Tetris.Managers
{
    public class ScoreController
    {
        private UIManager _uiManager;
        private ScoreConfig _scoreConfig;
        private PlayerStats _playerStats;

        public ScoreController(UIManager uiManager, PlayerStats playerStats, ScoreConfig scoreConfig)
        {
            _uiManager = uiManager;
            _scoreConfig = scoreConfig;
            _playerStats = playerStats;
            
            _playerStats.OnLevelChange += _uiManager.GameView.UpdateLevelCounter;
            _playerStats.OnLinesChange += _uiManager.GameView.UpdateLinesCounter;
            _playerStats.OnScoreChange += _uiManager.GameView.UpdateScoreCounter;
        
            ResetProgress();
        }

        public void CollectPlacedBlock()
        {
            _playerStats.Score +=_scoreConfig.GetRandomPlacedBlockReward();
        }
    
        public void CollectClearedRow(int rowsAmount)
        {
            _playerStats.Lines += rowsAmount;
            _playerStats.Score += _scoreConfig.GetClearedLRowsReward(rowsAmount);
        }

        public (int level, int lines, int score) GetResults()
        {
            return (_playerStats.Level, _playerStats.Lines, _playerStats.Score);
        }
        
        public void ResetProgress()
        {
            _playerStats.Score = 0;
            _playerStats.Level = 0;
            _playerStats.Lines = 0;
        }
    }
}

