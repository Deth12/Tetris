using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Tooltip("Parent transform where new blocks will be spawned")]
	[SerializeField] private Transform _blockContainer = default;
	[SerializeField] private Block[] _blockTypes = default;
	
    private int _nextBlockIndex;

	private void Start () 
	{
		SpawnBlock();
	}

	private Block CreateBlock(Vector3 position) 
    {
        Block block = Instantiate(_blockTypes[_nextBlockIndex], position, Quaternion.identity);
		block.transform.SetParent(_blockContainer);
        return block;
    }

	public void SpawnBlock() 
	{
        CreateBlock(transform.position);
        _nextBlockIndex = Random.Range(0, _blockTypes.Length);
	}
}
