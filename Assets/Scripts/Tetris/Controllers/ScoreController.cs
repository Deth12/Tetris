using System;
using Tetris.Configs;
using Tetris.Controllers;
using UnityEngine;

namespace Tetris.Managers
{
    public class ScoreController : IScoreController
    {
        private ScoreConfig _scoreConfig;
        private PlayerStats _playerStats;

        private int _scoreToNextLevel;
        
        public Action<float> OnDifficultyIncrease;
        public Action<int> OnCheckLinesToNextLevel;

        public ScoreController(PlayerStats playerStats, ScoreConfig scoreConfig)
        {
            _scoreConfig = scoreConfig;
            _playerStats = playerStats;
        }

        public void Initialize()
        {
            _playerStats.OnLinesChange += CheckLines;
            _playerStats.ResetProgress();
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

        private void CheckLines(int currentLines)
        {
            var linesToNextLevel = _scoreConfig.GetNextLevelLinesGap(_playerStats.Level) - currentLines;
            if (linesToNextLevel <= 0)
            {
                _playerStats.Level++;
                OnDifficultyIncrease?.Invoke(_scoreConfig.DifficultyIncreasePerLevel);
                linesToNextLevel = _scoreConfig.GetNextLevelLinesGap(_playerStats.Level);
            }
            OnCheckLinesToNextLevel?.Invoke(linesToNextLevel);
        }
        
        public void Dispose()
        {
            _playerStats.OnScoreChange -= CheckLines;
        }
    }
}

