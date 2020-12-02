using System;
using UnityEngine;

namespace Tetris.Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockShape _blockShape;

        private GridManager _gridManager;
        
        public event Action<float> OnBlockMove;
        public event Action OnBlockStrafe;
        public event Action OnBlockRotate;
        public event Action OnBlockPlaced;
        
        public bool Initialize(GridManager gridManager)
        {
            _gridManager = gridManager;
            if (!_gridManager.IsValidPositionOnGrid(transform)) 
            {
                return false;
            } 
            _gridManager.UpdateGrid(transform);
            return true;
        }
    
        public void TryMoveHorizontal(Vector3 movement) 
        {
            transform.position += movement;
            if (_gridManager.IsValidPositionOnGrid(transform))
            {
                OnBlockStrafe?.Invoke();
                _gridManager.UpdateGrid(transform);
            }
            else
            {
                transform.position -= movement;
            }
        }
        
        public void TryMoveVertical() 
        {
            transform.position += new Vector3(0, -1, 0);
            if (_gridManager.IsValidPositionOnGrid(transform))
            {
                _gridManager.UpdateGrid(transform);
                OnBlockMove?.Invoke(Time.time);
            } 
            else 
            {
                transform.position += new Vector3(0, 1, 0);
                OnBlockPlaced?.Invoke();
                
                OnBlockMove = null;
                OnBlockPlaced = null;
                this.enabled = false;
            }
        }
    
        public void TryRotate(float angle)
        {
            transform.Rotate(0, 0, angle);
            if (_gridManager.IsValidPositionOnGrid(transform) && _blockShape != BlockShape.Shape_O)
            {
                _gridManager.UpdateGrid(transform);
                OnBlockRotate?.Invoke();
            }
            else
            {
                transform.Rotate(0, 0, -angle);
            }
        }
    }
}
