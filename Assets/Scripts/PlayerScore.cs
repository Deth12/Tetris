using System;

public static class PlayerScore
{
    private static int _score;

    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange?.Invoke(_score);
        }
    }
    
    private static int _level;

    public static int Level
    {
        get => _level;
        set
        {
            _level = value;
            OnLevelChange?.Invoke(_level);
        }
    }
    
    private static int _lines;

    public static int Lines
    {
        get => _lines;
        set
        {
            _lines = value;
            OnLinesChange?.Invoke(_lines);
        }
    }

    public static Action<int> OnScoreChange;
    public static Action<int> OnLevelChange;
    public static Action<int> OnLinesChange;
    
    public static void ResetProgress()
    {
        _score = 0;
        _level = 0;
        _lines = 0;
    }
}
