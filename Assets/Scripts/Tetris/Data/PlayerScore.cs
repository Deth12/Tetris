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

    public static event Action<int> OnScoreChange;
    public static event Action<int> OnLevelChange;
    public static event Action<int> OnLinesChange;
    
    public static void ResetProgress()
    {
        Score = 0;
        Level = 0;
        Lines = 0;
    }
}
