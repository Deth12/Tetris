using UnityEngine;
using Tetris.Blocks;

namespace Tetris.Configs
{
    [CreateAssetMenu(menuName = "Tetris/Configs/BlocksConfig", fileName = "BlocksConfig")]
    public class BlocksConfig : ScriptableObject
    {
        [SerializeField] private Block[] _blockTypes = default;

        public Block[] BlockTypes => _blockTypes;
    }
}

