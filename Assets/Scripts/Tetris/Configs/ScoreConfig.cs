using UnityEngine;

namespace Tetris.Configs
{
    [CreateAssetMenu(menuName = "Tetris/Configs/ScoreConfig", fileName = "ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [Tooltip("Lines gap needed to reach for next level (multiplied by level)")]
        [SerializeField] private int _levelIncreaseLinesGap = 2;
        
        [Range(0,1f)]
        [Tooltip("Block fall speed increase each new level (in percents)")]
        [SerializeField] private float _difficultyIncreasePerLevel = 0.05f;
        
        [Tooltip("Maximum amount of score points for placing block ")]
        [SerializeField] private int _placedBlockMaxReward = 8;
        
        [Tooltip("Minimum amount of score points for placing block ")]
        [SerializeField] private int _placedBlockMinReward = 4;
        
        [Tooltip("Amount of score points for clearing the whole row")]
        [SerializeField] private int _rowClearReward = 20;

        public int LevelIncreaseLinesGap => _levelIncreaseLinesGap;
        public float DifficultyIncreasePerLevel => _difficultyIncreasePerLevel;
        public int PlacedBlockMinReward => _placedBlockMinReward;
        public int PlacedBlockMaxReward => _placedBlockMaxReward;
        public int RowClearReward => _rowClearReward;

        public int GetRandomPlacedBlockReward()
        {
            return Random.Range(PlacedBlockMinReward, PlacedBlockMaxReward + 1);
        }

        public int GetClearedLinesReward(int linesAmount)
        {
            return RowClearReward * linesAmount;
        }

        public int GetNextLevelLinesGap(int level)
        {
            return level * LevelIncreaseLinesGap;
        }
    }
}
