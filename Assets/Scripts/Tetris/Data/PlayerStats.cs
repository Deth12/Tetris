using System;

[Serializable]
public class PlayerStats
{
    private int _score;
    private int _level;
    private int _lines;

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
}
