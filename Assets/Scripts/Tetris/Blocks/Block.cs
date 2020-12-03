using System;
using UnityEngine;
using Tetris.Controllers;

namespace Tetris.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockShape _blockShape = default;

        private GridController _gridController;
        
        public event Action<float> OnBlockFall;
        public event Action OnBlockStrafe;
        public event Action OnBlockRotate;
        public event Action OnBlockPlaced;
        
        public bool TrySetup(GridController gridController)
        {
            _gridController = gridController;
            if (!_gridController.IsValidPositionOnGrid(transform)) 
            {
                return false;
            } 
            _gridController.UpdateGrid(transform);
            return true;
        }
    
        public void TryMoveHorizontal(Vector3 movement) 
        {
            transform.position += movement;
            if (_gridController.IsValidPositionOnGrid(transform))
            {
                OnBlockStrafe?.Invoke();
                _gridController.UpdateGrid(transform);
            }
            else
            {
                transform.position -= movement;
            }
        }
        
        public void TryMoveVertical(Vector3 movement)
        {
            transform.position += movement;
            if (_gridController.IsValidPositionOnGrid(transform))
            {
                _gridController.UpdateGrid(transform);
                OnBlockFall?.Invoke(Time.time);
            } 
            else 
            {
                transform.position -= movement;
                
                OnBlockPlaced?.Invoke();
                
                OnBlockFall = null;
                OnBlockStrafe = null;
                OnBlockRotate = null;
                OnBlockPlaced = null;
                this.enabled = false;
            }
        }
    
        public void TryRotate(float angle)
        {
            transform.Rotate(0, 0, angle);
            if (_gridController.IsValidPositionOnGrid(transform) && _blockShape != BlockShape.Shape_O)
            {
                _gridController.UpdateGrid(transform);
                OnBlockRotate?.Invoke();
            }
            else
            {
                transform.Rotate(0, 0, -angle);
            }
        }
    }
}
