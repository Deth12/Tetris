using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetris.Blocks;
using Tetris.Managers;

namespace Tetris.Controllers
{
    public class BlockController : MonoBehaviour
{
    [SerializeField] private Spawner _spawner = default;
    [SerializeField] private InputManager _inputManager = default;
    [SerializeField] private ScoreManager _scoreManager = default;
    [SerializeField] private GameController _gameController = default;
    
    [Header("SFX")]
    [SerializeField] private AudioClip _blockMove = default;
    [SerializeField] private AudioClip _blockRotate = default;
    [SerializeField] private AudioClip _blockDrop = default;
    
    private float _fallTick = 0.8f;

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
            
            _currentBlock.OnBlockStrafe += () =>
            {
                AudioManager.Instance.PlayClip(_blockMove);
            };
            
            _currentBlock.OnBlockRotate += () =>
            {
                AudioManager.Instance.PlayClip(_blockRotate);
            };
            
            _currentBlock.OnBlockPlaced += () =>
            {
                AudioManager.Instance.PlayClip(_blockDrop);
                _scoreManager.CollectPlacedBlock();
                _scoreManager.CollectClearedRow(Board.DeleteFullRows());
                _spawner.SpawnBlock();
            };
        }
        else
        {
            _currentBlock = null;
            _gameController.GameOver();
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
        if(!GameController.IsGameActive || _currentBlock == null)
            return;
        
        if (Time.time - _lastFallTime >= _fallTick)
        {
            _currentBlock.TryMoveVertical();
        }
    }
}
}

