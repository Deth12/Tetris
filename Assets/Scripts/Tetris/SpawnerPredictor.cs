using UnityEngine;

public class SpawnerPredictor : MonoBehaviour
{
    [SerializeField] private Transform _nextBlockParent = default;

    private GameObject _nextBlock;
    
    public void GeneratePrediction(Block nextBlockPrefab)
    {
        if (_nextBlock != null)
        {
            Destroy(_nextBlock.gameObject);
        }

        _nextBlock = Instantiate(nextBlockPrefab, _nextBlockParent).gameObject;
        Utils.AlignCenter(_nextBlock);
        Destroy(_nextBlock.GetComponent<Block>());
    }
}
