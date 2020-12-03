using System;
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
        
        private Block _currentBlock;

        private float _fallTick = 0.8f;
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
            if (block.TrySetup(_gridManager))
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
                AudioManager.Instance.PlayClip(_blockMove);
            };
                
            block.OnBlockRotate += () =>
            {
                AudioManager.Instance.PlayClip(_blockRotate);
            };
                
            block.OnBlockPlaced += () =>
            {
                AudioManager.Instance.PlayClip(_blockDrop);
                _scoreController.CollectPlacedBlock();
                _scoreController.CollectClearedRow(_gridManager.DeleteFullRows());
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

