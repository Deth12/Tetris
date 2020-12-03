using System;
using UnityEngine;
using Tetris.Blocks;
using Tetris.Managers;
using Tetris.Constants;

namespace Tetris.Controllers
{ 
    public class BlockController : IBlockController
    {
        private ScoreController _scoreController;
        private InputController _inputController;

        private SpawnManager _spawnManager;
        private GridController _gridController;
        
        private Block _currentBlock;

        private float _fallTick = 1f;
        private float _lastFallTime;
        
        public event Action OnBlockStuck;

        public BlockController(ScoreController scoreController,
            InputController inputController, GridController gridController, SpawnManager spawnManager)
        {
            _scoreController = scoreController;
            _inputController = inputController;
            _spawnManager = spawnManager;
            _gridController = gridController;
        }
            
        public void Initialize()
        {
            _spawnManager.OnBlockSpawn += SetControlledBlock;
            _scoreController.OnDifficultyIncrease += IncreaseFallSpeed;
            SetupInput();
        }
        
        private void SetControlledBlock(Block block)
        {
            if (block.TrySetup(_gridController))
            {
                _lastFallTime = Time.time;
                AssignBlockEvents(block);
                _currentBlock = block;
            }
            else
            {
                _currentBlock = null;
                OnBlockStuck?.Invoke();
            }
        }

        private void AssignBlockEvents(Block block)
        {
            block.OnBlockMove += (time) =>
            {
                _lastFallTime = time;
            };
                
            block.OnBlockStrafe += () =>
            {
                AudioManager.Instance.PlayClipByName(ConstantAudioNames.BLOCK_MOVE);
            };
                
            block.OnBlockRotate += () =>
            {
                AudioManager.Instance.PlayClipByName(ConstantAudioNames.BLOCK_ROTATE);
            };
                
            block.OnBlockPlaced += () =>
            {
                AudioManager.Instance.PlayClipByName(ConstantAudioNames.BLOCK_DROP);
                _scoreController.CollectPlacedBlock();
                _scoreController.CollectClearedRow(_gridController.DeleteFullRows());
                _spawnManager.SpawnBlock();
            };
        }

        private void SetupInput()
        {
            _inputController.OnMoveLeftPress += MoveLeft;
            _inputController.OnMoveRightPress += MoveRight;
            _inputController.OnRotatePress += Rotate;
            _inputController.OnFallPress += ForceFall;
        }
        
        private void MoveLeft()
        {
            if (_currentBlock != null)
            {
                _currentBlock.TryMoveHorizontal(Vector3.left);
            }
        }

        private void MoveRight()
        {
            if (_currentBlock != null)
            {
                _currentBlock.TryMoveHorizontal(Vector3.right);
            }
        }

        private void Rotate()
        {
            if (_currentBlock != null)
            {
                _currentBlock.TryRotate(-90f);
            }
        }

        private void ForceFall()
        {
            if (_currentBlock != null)
            {
                _currentBlock.TryMoveVertical(Vector3.down);
            }
        }

        private void IncreaseFallSpeed(float multiplier)
        {
            _fallTick *= (1 - (_fallTick * multiplier));
        }

        public void Tick()
        {
            if (!GameController.IsGameActive || _currentBlock == null)
            {
                return;
            }
            
            if (Time.time - _lastFallTime >= _fallTick)
            {
                _currentBlock.TryMoveVertical(Vector3.down);
            }
        }
    }
}

