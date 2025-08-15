
// còn nhiều thuật toán khác có thể tạo mê cung nên dùng interface
// xem https://www.youtube.com/watch?v=g7b0JUniSgs  để hiểu hơn về thuật toán

using System;
using System.Collections.Generic;

public interface IMazeGenerator 
{
    public Node[,] Generate(int width, int height);
}

public class EllersMazeGenerator : IMazeGenerator
{
    private Random random = new System.Random();
    
    public Node[,] Generate(int width, int height)
    {
        Node[,] maze = new Node[width, height];
        
        // tạo các node
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            maze[x, y] = new Node(x,y);

        int[] setIds = new int[width];
        int nextSetId = 1;

        for (int x = 0; x < width; x++)
            setIds[x] = nextSetId++;

        for (int y = 0; y < height; y++)
        {
            
            // chiều ngang
            for (int x = 0; x < width - 1; x++)
            {
                if (setIds[x] != setIds[x + 1] && random.Next(2) == 0)
                {
                    MergeSets(setIds,setIds[x],setIds[x + 1]);
                    RemoveWallBetween(maze[x, y],maze[x + 1, y]);
                }
            }
            
            //nếu là hàng cuối thì merge tất cả set còn lại
            if (y == height - 1)
            {
                for (int x = 0; x < width - 1; x++)
                {
                    if (setIds[x] != setIds[x + 1])
                    {
                        MergeSets(setIds, setIds[x], setIds[x + 1]);
                        RemoveWallBetween(maze[x, y], maze[x + 1, y]);
                    }
                }
                break; 
            }
            
            // chiều dọc 
            Dictionary<int, List<int>> cellsInSet = new();

            for (int x = 0; x < width; x++)
            {
                int setId = setIds[x];
                if (!cellsInSet.ContainsKey(setId))
                {
                    cellsInSet.Add(setId, new List<int>());
                }
                cellsInSet[setId].Add(x);
            }

            if (y < height - 1)
            {
                bool[] connectedDown = new bool[width];

                foreach (var kvp in cellsInSet)
                {
                    var cells = kvp.Value;
                    int numConnections = random.Next(1,cells.Count + 1);
                    var shuffled = new List<int>(cells);
                    shuffled.Shuffle();
                    for (int i = 0; i < numConnections; i++)
                    {
                        int cx = shuffled[i];
                        RemoveWallBetween(maze[cx,y],maze[cx,y + 1]);
                        connectedDown[cx] = true;
                    }
                }

                for (int x = 0; x < width; x++)
                {
                    if (!connectedDown[x])
                    {
                        setIds[x] = nextSetId ++;
                    }
                }
            }
        }
        
        // node được chọn start và end có đường đi ra
        maze[0, 0].WallDown = false;
        maze[width - 1, height - 1].WallUp = false;

        return maze;
    }

    private void MergeSets(int[] sets,int from, int to)
    {
        for (int i = 0; i < sets.Length; i++)
        {
            if (sets[i] == to)
                sets[i] = from;
        }
    }

    private void RemoveWallBetween(Node a, Node b)
    {
        if (a.X == b.X)
        {
            if (a.Y < b.Y)
            {
                a.WallUp = false;
                b.WallDown = false;
            }
            else
            {
                a.WallDown = false;
                b.WallUp = false;
            }
        }
        else if(a.Y == b.Y)
        {
            if (a.X < b.X)
            {
                a.WallRight =  false;
                b.WallLeft = false;
            }
            else
            {
                a.WallLeft = false;
                b.WallRight = false;
            }
        }
        
        a.AddNeighbor(b);
        b.AddNeighbor(a);
    }
    
    
}

public static class Helper
{
    private static Random random = new System.Random();
    
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = random.Next(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
