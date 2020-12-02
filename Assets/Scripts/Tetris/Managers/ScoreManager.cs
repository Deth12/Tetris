using System;
using UnityEngine;
using Tetris.Controllers;
using Tetris.Configs;

namespace Tetris.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private UIController _uiController = default;
        [SerializeField] private ScoreConfig _scoreConfig = default;
        
        private void Start()
        {
            
            PlayerScore.OnLevelChange += _uiController.GameView.UpdateLevelCounter;
            PlayerScore.OnLinesChange += _uiController.GameView.UpdateLinesCounter;
            PlayerScore.OnScoreChange += _uiController.GameView.UpdateScoreCounter;
        
            PlayerScore.ResetProgress();
        }

        public void CollectPlacedBlock()
        {
            PlayerScore.Score += _scoreConfig.GetRandomPlacedBlockReward();
        }
    
        public void CollectClearedRow(int rowsAmount)
        {
            PlayerScore.Lines += rowsAmount;
            PlayerScore.Score += rowsAmount * _scoreConfig.ClearedRowReward;
        }

        public (int level, int lines, int score) GetResults()
        {
            return (PlayerScore.Level, PlayerScore.Lines, PlayerScore.Score);
        }
    }
}

