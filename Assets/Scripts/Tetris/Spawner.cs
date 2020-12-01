using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Tooltip("Parent transform where new blocks will be spawned")]
	[SerializeField] private Transform _blockContainer = default;
	[SerializeField] private Block[] _blockTypes = default;
	
	[SerializeField] private BlockController _blockController = default;
	[SerializeField] private SpawnerPredictor _predictor = default;

	private int _nextBlockIndex;
	
	public System.Action<Block> OnBlockSpawn;

	private void Start ()
	{
		_nextBlockIndex = Random.Range(0, _blockTypes.Length);
		SpawnBlock();
	}
	
	public void SpawnBlock()
	{
		OnBlockSpawn?.Invoke(CreateBlock(transform.position));
		_nextBlockIndex = Random.Range(0, _blockTypes.Length);
		_predictor.GeneratePrediction(_blockTypes[_nextBlockIndex]);
	}

	private Block CreateBlock(Vector3 position) 
    {
        Block block = Instantiate(_blockTypes[_nextBlockIndex], position, Quaternion.identity);
		block.transform.SetParent(_blockContainer);
        return block;
    }
}
