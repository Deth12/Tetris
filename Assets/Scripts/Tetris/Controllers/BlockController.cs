using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private float _fallTick = 1f;

    private Block _currentBlock;
    
    private float _lastFallTime;
    
    private void Awake()
    {
        _spawner.OnBlockSpawn += SetControlledBlock;
    }
    
    private void SetControlledBlock(Block block)
    {
        if (block.Initialize(this))
        {
            _lastFallTime = Time.time;
            _currentBlock = block;
            
            _currentBlock.OnBlockMove += (time) =>
            {
                _lastFallTime = time;
            };

            _currentBlock.OnBlockPlaced += () =>
            {
                _scoreManager.CollectBlockPlaced();
                _scoreManager.CollectRowsCleared(Board.DeleteFullRows());
                _spawner.SpawnBlock();
            };
        }
        else
        {
            // Game Over
        }
    }

    private void Start()
    {
        SetupInput();
    }

    private void SetupInput()
    {
        _inputManager.OnLeftPress += MoveLeft;
        _inputManager.OnRightPress += MoveRight;
        _inputManager.OnUpPress += Rotate;
        _inputManager.OnDownPress += ForceFall;
    }
    
    private void MoveLeft()
    {
        _currentBlock?.TryMoveHorizontal(new Vector3(-1, 0, 0));
    }

    private void MoveRight()
    {
        _currentBlock?.TryMoveHorizontal(new Vector3(1, 0, 0));
    }

    private void Rotate()
    {
        _currentBlock?.TryRotate(-90f);
    }

    private void ForceFall()
    {
        _currentBlock?.TryMoveVertical();
    }

    private void Update()
    {
        if (Time.time - _lastFallTime >= _fallTick)
        {
            _currentBlock.TryMoveVertical();
        }
    }
}
