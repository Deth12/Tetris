using UnityEngine;

namespace Tetris.Configs
{
    [CreateAssetMenu(menuName = "Tetris/Configs/ScoreConfig", fileName = "ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [SerializeField] private int _placedBlockMaxReward = 8;
        [SerializeField] private int _placedBlockMinReward = 4;
        [SerializeField] private int _clearedRowReward = 20;

        public int PlacedBlockMinReward => _placedBlockMinReward;
        public int PlacedBlockMaxReward => _placedBlockMaxReward;
        public int ClearedRowReward => _clearedRowReward;

        public int GetRandomPlacedBlockReward()
        {
            return Random.Range(_placedBlockMinReward, _placedBlockMaxReward + 1);
        }
    }
}
