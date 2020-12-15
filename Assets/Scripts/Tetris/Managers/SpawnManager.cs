using UnityEngine;
using Tetris.Blocks;
using Tetris.Configs;
using Tetris.Extensions;
using Zenject;

namespace Tetris.Managers
{
	public class SpawnManager : MonoBehaviour
	{
		[SerializeField] private Transform _blockSpawnPoint = default;
		[Tooltip("Parent transform where new blocks will be spawned")]
		[SerializeField] private Transform _blockContainer = default;
		[Tooltip("Parent transform where next block will be spawned")]
		[SerializeField] private Transform _nextBlockContainer = default;

		private BlocksConfig _blocksConfig;
		private int _nextBlockIndex;
		private GameObject _nextBlock;

		public event System.Action<Block> OnBlockSpawn;

		[Inject]
		private void Construct(BlocksConfig blocksConfig)
		{
			_blocksConfig = blocksConfig;
		}

		private void Start ()
		{
			_nextBlockIndex = Random.Range(0, _blocksConfig.BlockTypes.Length);
		}
	
		public void SpawnBlock()
		{
			OnBlockSpawn?.Invoke(CreateBlock(_blockSpawnPoint.position));
			_nextBlockIndex = Random.Range(0, _blocksConfig.BlockTypes.Length);
			SpawnBlockPrediction(_nextBlockIndex);
		}
	
		private Block CreateBlock(Vector3 position) 
		{
			Block block = Instantiate(_blocksConfig.BlockTypes[_nextBlockIndex], position, Quaternion.identity);
			block.transform.SetParent(_blockContainer);
			return block;
		}

		private void SpawnBlockPrediction(int blockIndex)
		{
			if (_nextBlock != null)
			{
				Destroy(_nextBlock.gameObject);
			}

			var position = _nextBlockContainer.position;
			_nextBlock = Instantiate(_blocksConfig.BlockTypes[blockIndex], position, Quaternion.identity).gameObject;
			_nextBlock.transform.SetParent(_nextBlockContainer);
			_nextBlock.AlignCenter();
			
			Destroy(_nextBlock.GetComponent<Block>());
		}
	}
}

