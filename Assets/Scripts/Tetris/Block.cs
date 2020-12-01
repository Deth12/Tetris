using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockShape _blockShape;
    
    public Action<float> OnBlockMove;
    public Action OnBlockPlaced;
    
    public bool Initialize(BlockController blockController)
    {
        if (!Board.IsValidPositionOnGrid(transform)) 
        {
            return false;
        } 
        Board.UpdateGrid(transform);
        return true;
    }

    public void TryMoveHorizontal(Vector3 movement) 
    {
        transform.position += movement;
        if (Board.IsValidPositionOnGrid(transform))
        {
            Board.UpdateGrid(transform);
        }
        else
        {
            transform.position -= movement;
        }
    }
    
    public void TryMoveVertical() 
    {
        transform.position += new Vector3(0, -1, 0);
        if (Board.IsValidPositionOnGrid(transform))
        {
            Board.UpdateGrid(transform);
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
        if (Board.IsValidPositionOnGrid(transform) && _blockShape != BlockShape.Shape_O)
        {
            Board.UpdateGrid(transform);
        }
        else
        {
            transform.Rotate(0, 0, -angle);
        }
    }
}
