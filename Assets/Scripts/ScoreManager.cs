using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    [Header("Rewards")]
    [SerializeField] private int _blockPlacedMaxReward = 8;
    [SerializeField] private int _blockPlacedMinReward = 4;
    [SerializeField] private int _rowClearedReward = 20;
    
    private void Start()
    {
        PlayerScore.OnLevelChange += _uiController.UpdateScoreCounter;
        PlayerScore.OnLinesChange += _uiController.UpdateLinesCounter;
        PlayerScore.OnScoreChange += _uiController.UpdateScoreCounter;
        
        PlayerScore.ResetProgress();
    }

    public void CollectBlockPlaced()
    {
        PlayerScore.Score += Random.Range(_blockPlacedMinReward, _blockPlacedMaxReward + 1);
    }
    
    public void CollectRowsCleared(int rowsAmount)
    {
        PlayerScore.Lines += rowsAmount;
        PlayerScore.Score += rowsAmount * _rowClearedReward;
    }
}
