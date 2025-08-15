using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeView : MonoBehaviour
{
    [SerializeField] private GameObject wallPf;
    [SerializeField] private GameObject floorPf;
    [SerializeField] private float cellSize = 2f;

    public float CellSize => cellSize;

    public void BuildMaze(Node[,] maze)
    {
        int widthLength  = maze.GetLength(0);
        int heightLength = maze.GetLength(1);

        for (int y = 0; y < heightLength; y++)
        {
            for (int x = 0; x < widthLength; x++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
            
                var floor = Instantiate(floorPf, pos, Quaternion.identity, transform);
                floor.transform.localScale = new Vector3(cellSize, 0.01f, cellSize);

                var node = maze[x, y];
                
                if (node.WallUp) CreateWall(pos + new Vector3(0, 0, cellSize / 2), 0);
                if (node.WallDown) CreateWall(pos + new Vector3(0, 0, -cellSize / 2), 0);
                if (node.WallRight) CreateWall(pos + new Vector3(cellSize / 2, 0, 0), 90);
                if (node.WallLeft)  CreateWall(pos + new Vector3(-cellSize / 2, 0, 0), 90);
            }
        }
    }
    
    private void CreateWall(Vector3 pos, float yRot)
    {
        float wallThickness = cellSize * 0.2f; // tường dày 20% cell
        var wall = Instantiate(wallPf, pos + Vector3.up * 0.5f, Quaternion.Euler(0, yRot, 0), transform);
        wall.transform.localScale = new Vector3(cellSize, 1, wallThickness);
    }
}
