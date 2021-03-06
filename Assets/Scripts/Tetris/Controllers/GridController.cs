﻿using UnityEngine;

namespace Tetris.Controllers
{
    public class GridController
    {
        private const int WIDTH = 10;
        private const int HEIGHT = 20;
        
        private Transform[,] grid = new Transform[WIDTH, HEIGHT];

        public Vector2 RoundVector2(Vector2 v) 
        {
            return new Vector2 (Mathf.Round (v.x), Mathf.Round (v.y));
        }

        public bool IsValidPositionOnGrid(Transform transform)
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
        
        public void UpdateGrid(Transform transform) 
        {
            for (int y = 0; y < HEIGHT; ++y) 
            {
                for (int x = 0; x < WIDTH; ++x) 
                {
                    if (grid[x,y] != null && grid[x,y].parent == transform) 
                    {
                        grid[x,y] = null;
                    }
                } 
            }
            foreach (Transform child in transform) 
            {
                Vector2 v = RoundVector2(child.position);
                grid[(int)v.x,(int)v.y] = child;
            }
        }

        public bool InsideBorder(Vector2 pos) 
        {
            return ((int)pos.x >= 0 && (int)pos.x < WIDTH && (int)pos.y >= 0);
        }

        public void DeleteLine(int y) 
        {
            for (int x = 0; x < WIDTH; x++) 
            {
                GameObject.Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }

        public void DecreaseLine(int y) 
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

        public void DecreaseLineAbove(int y) 
        {
            for (int i = y; i < HEIGHT; i++) 
            {
                DecreaseLine(i);
            }
        }

        public bool IsLineFull(int y)
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

        public int DeleteFullLines()
        {
            int rowsCleared = 0;
            for (int y = 0; y < HEIGHT; y++) 
            {
                if (IsLineFull(y))
                {
                    rowsCleared++;
                    DeleteLine(y);
                    DecreaseLineAbove(y + 1);
                    --y;
                }
            }

            return rowsCleared;
        }
    }
}

