using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tetris.Blocks;
using Tetris.Managers;

namespace Tetris.Controllers
{ 
    public class BlockController : IBlockController
    {
        private ScoreController _scoreController;
        private InputController _inputController;

        private SpawnManager _spawnManager;
        private GridManager _gridManager;
        
        [Header("SFX")]
        [SerializeField] private AudioClip _blockMove = default;
        [SerializeField] private AudioClip _blockRotate = default;
        [SerializeField] private AudioClip _blockDrop = default;
        
        private float _fallTick = 0.8f;

        private Block _currentBlock;
        
        private float _lastFallTime;
        
        public event Action OnBlockStuck;

        public BlockController(ScoreController scoreController,
            InputController inputController, SpawnManager spawnManager, GridManager gridManager)
        {
            _scoreController = scoreController;
            _inputController = inputController;
            _spawnManager = spawnManager;
            _gridManager = gridManager;
        }
            
        public void Initialize()
        {
            _spawnManager.OnBlockSpawn += SetControlledBlock;
            SetupInput();
        }
        
        private void SetControlledBlock(Block block)
        {
            if (block.Initialize(_gridManager))
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
                    _scoreController.CollectPlacedBlock();
                    _scoreController.CollectClearedRow(_gridManager.DeleteFullRows());
                    _spawnManager.SpawnBlock();
                };
            }
            else
            {
                _currentBlock = null;
                OnBlockStuck?.Invoke();
            }
        }

        private void SetupInput()
        {
            _inputController.OnLeftPress += MoveLeft;
            _inputController.OnRightPress += MoveRight;
            _inputController.OnUpPress += Rotate;
            _inputController.OnDownPress += ForceFall;
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

        public void Tick()
        {
            if (!GameController.IsGameActive || _currentBlock == null)
            {
                return;
            }
            
            if (Time.time - _lastFallTime >= _fallTick)
            {
                _currentBlock.TryMoveVertical();
            }
        }
    }
}

