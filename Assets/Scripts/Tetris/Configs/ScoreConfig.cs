using UnityEngine;

namespace Tetris.Configs
{
    [CreateAssetMenu(menuName = "Tetris/Configs/ScoreConfig", fileName = "ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [Tooltip("Maximum amount of score points for placing block ")]
        [SerializeField] private int _placedBlockMaxReward = 8;
        [Tooltip("Minimum amount of score points for placing block ")]
        [SerializeField] private int _placedBlockMinReward = 4;
        [Tooltip("Amount of score points for clearing the whole row")]
        [SerializeField] private int _rowClearReward = 20;

        public int PlacedBlockMinReward => _placedBlockMinReward;
        public int PlacedBlockMaxReward => _placedBlockMaxReward;
        public int RowClearReward => _rowClearReward;

        public int GetRandomPlacedBlockReward()
        {
            return Random.Range(_placedBlockMinReward, _placedBlockMaxReward + 1);
        }

        public int GetClearedLRowsReward(int rowsAmount)
        {
            return _rowClearReward * rowsAmount;
        }
    }
}
