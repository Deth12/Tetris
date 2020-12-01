using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float _fallTick = 1f;
    private float _lastFallTime;
    
    private float _lastKeyDown;
    private float _timeKeyPressed;

    private void Start () 
    {
        _lastFallTime = Time.time;
        _lastKeyDown = Time.time;
        _timeKeyPressed = Time.time;
        
        if (IsValidPositionOnGrid()) 
        {
            InsertOnGrid();
        } 
        else 
        { 
            throw new Exception("Failed to place initial block.");
        }
    }
    
    private bool IsValidPositionOnGrid() 
    {
        foreach (Transform child in transform) 
        {
            Vector2 v = Board.RoundVector2(child.position);
            if(!Board.InsideBorder(v)) 
            {
                return false;
            }
            if (Board.grid[(int)(v.x), (int)(v.y)] != null &&
                Board.grid[(int)(v.x), (int)(v.y)].parent != transform) 
            {
                return false;
            }
        }
        return true;
    }
    
    private void InsertOnGrid() 
    {
        foreach (Transform child in transform) 
        {
            Vector2 v = Board.RoundVector2(child.position);
            Board.grid[(int)v.x,(int)v.y] = child;
        }
    }
    
    private void Update()
    {
        if (GetKey(KeyCode.LeftArrow))
        {
            TryMove(new Vector3(-1, 0, 0));
        }
        else if (GetKey(KeyCode.RightArrow))
        {
            TryMove(new Vector3(1, 0, 0));
        }
        else if (GetKey(KeyCode.UpArrow))
        {
            TryRotate(-90);

        }
        else if (GetKey(KeyCode.DownArrow) || (Time.time - _lastFallTime) >= _fallTick)
        {
            Fall();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            while (this.enabled)
            {
                Fall();
            }
        }
    }
    
    private bool GetKey(KeyCode key) 
    {
        bool keyDown = Input.GetKeyDown(key);
        bool pressed = Input.GetKey(key) && Time.time - _lastKeyDown > 0.5f && Time.time - _timeKeyPressed > 0.05f;
        if (keyDown) 
        {
            _lastKeyDown = Time.time;
        }
        if (pressed) 
        {
            _timeKeyPressed = Time.time;
        }
        return keyDown || pressed;
    }
    
    private void TryMove(Vector3 movement) 
    {
        transform.position += movement;
        if (IsValidPositionOnGrid())
        {
            UpdateGrid();
        }
        else
        {
            // Revert movement
            transform.position -= movement;
        }
    }

    private void TryRotate(float angle)
    {
        transform.Rotate(0, 0, angle);
        if (IsValidPositionOnGrid())
        {
            UpdateGrid();
        }
        else
        {
            // Revert rotation
            transform.Rotate(0, 0, -angle);
        }
    }
    
    private void UpdateGrid() 
    {
        for (int y = 0; y < Board.h; ++y) 
        {
            for (int x = 0; x < Board.w; ++x) 
            {
                if (Board.grid[x,y] != null && Board.grid[x,y].parent == transform) 
                {
                    Board.grid[x,y] = null;
                }
            } 
        }
        InsertOnGrid();
    }
    
    private void Fall() 
    {
        transform.position += new Vector3(0, -1, 0);
        if (IsValidPositionOnGrid())
        {
            UpdateGrid();
        } 
        else 
        {
            transform.position += new Vector3(0, 1, 0);
            Board.DeleteFullRows();
            FindObjectOfType<Spawner>().SpawnBlock();
            this.enabled = false;
        }
        _lastFallTime = Time.time;
    }
    
    public void AlignCenter() 
    {
        transform.position += transform.position - Utils.Center(gameObject);
    }

    public void GameOver() 
    {
        Debug.Log("GAME OVER!");
        while (!IsValidPositionOnGrid()) 
        {
            transform.position  += new Vector3(0, 1, 0);
        } 
        UpdateGrid();
        this.enabled = false;
    }
}
