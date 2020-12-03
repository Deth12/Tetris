using System;

namespace Tetris.Data
{
    [Serializable]
    public class PlayerStats
    {
        private int _score = 0;
        private int _level = 1;
        private int _lines = 0;
    
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChange?.Invoke(_score);
            }
        }
        
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnLevelChange?.Invoke(_level);
            }
        }
        
        public int Lines
        {
            get => _lines;
            set
            {
                _lines = value;
                OnLinesChange?.Invoke(_lines);
            }
        }
        
        public Action<int> OnLevelChange;
        public Action<int> OnLinesChange;
        public Action<int> OnScoreChange;
        
        public void ResetProgress()
        {
            Score = 0;
            Level = 1;
            Lines = 0;
        }
    }
}

