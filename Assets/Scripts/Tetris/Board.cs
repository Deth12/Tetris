using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private const int WIDTH = 10;
    private const int HEIGHT = 20;
    
    private static Transform[,] grid = new Transform[WIDTH, HEIGHT];
    
    public static Action<int> OnRowsClear;
    
    public static Vector2 RoundVector2(Vector2 v) 
    {
        return new Vector2 (Mathf.Round (v.x), Mathf.Round (v.y));
    }

    public static bool IsValidPositionOnGrid(Transform transform)
    {
        foreach (Transform child in transform) 
        {
            Vector2 v = RoundVector2(child.position);
            if(!InsideBorder(v)) 
            {
                return false;
            }
            if (grid[(int)(v.x), (int)(v.y)] != null &&
                grid[(int)(v.x), (int)(v.y)].parent != transform) 
            {
                return false;
            }
        }
        return true;
    }
    
    public static void UpdateGrid(Transform transform) 
    {
        for (int y = 0; y < HEIGHT; ++y) 
        {
            for (int x = 0; x < Board.WIDTH; ++x) 
            {
                if (Board.grid[x,y] != null && Board.grid[x,y].parent == transform) 
                {
                    Board.grid[x,y] = null;
                }
            } 
        }
        foreach (Transform child in transform) 
        {
            Vector2 v = Board.RoundVector2(child.position);
            grid[(int)v.x,(int)v.y] = child;
        }
    }

    public static bool InsideBorder(Vector2 pos) 
    {
        return ((int)pos.x >= 0 && (int)pos.x < WIDTH && (int)pos.y >= 0);
    }

    public static void DeleteRow(int y) 
    {
        for (int x = 0; x < WIDTH; x++) 
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DecreaseRow(int y) 
    {
        for (int x = 0; x < WIDTH; x++) 
        {
            if (grid[x, y] != null) 
            {
                grid[x, y - 1] = grid[x, y];
                grid[x,y] = null;
                grid[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DecreaseRowAbove(int y) 
    {
        for (int i = y; i < HEIGHT; i++) 
        {
            DecreaseRow(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < WIDTH; x++) 
        {
            if (grid[x, y] == null) 
            {
                return false;
            }
        }
        return true;
    }

    public static int DeleteFullRows()
    {
        int rowsCleared = 0;
        for (int y = 0; y < HEIGHT; y++) 
        {
            if (IsRowFull(y))
            {
                rowsCleared++;
                DeleteRow(y);
                DecreaseRowAbove(y + 1);
                // TODO: Add score (+= (h-y) * 10)
                --y;
            }
        }

        return rowsCleared;
    }
}
