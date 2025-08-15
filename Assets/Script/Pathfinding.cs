using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding
{
     public static List<Node> FindPath(Node[,] maze, Node startNode, Node goalNode)
    {
        HashSet<Node> openSet = new();
        HashSet<Node> closedSet = new();

        startNode.GCost = 0;
        startNode.HCost = GetDistance(startNode, goalNode);
        startNode.CameFrom = null;

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Lấy node có FCost nhỏ nhất
            Node current = openSet.OrderBy(n => n.FCost).ThenBy(n => n.HCost).First();

            if (current == goalNode)
                return RetracePath(startNode, goalNode);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Node neighbor in GetNeighbors(maze, current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGCost = current.GCost + GetDistance(current, neighbor);

                if (tentativeGCost < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.HCost = GetDistance(neighbor, goalNode);
                    neighbor.CameFrom = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // Không tìm thấy đường
    }

    private static List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(current);
            current = current.CameFrom;
        }
        path.Add(startNode);
        path.Reverse();
        return path;
    }

    private static float GetDistance(Node a, Node b)
    {
        // Manhattan để di chuyển 4 hướng
        return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
    }

    private static IEnumerable<Node> GetNeighbors(Node[,] maze, Node node)
    {
        int width = maze.GetLength(0);
        int height = maze.GetLength(1);
        
        if (!node.WallUp && node.Y + 1 < height)
            yield return maze[node.X, node.Y + 1];
        
        if (!node.WallDown && node.Y - 1 >= 0)
            yield return maze[node.X, node.Y - 1];

        if (!node.WallLeft && node.X - 1 >= 0)
            yield return maze[node.X - 1, node.Y];

        if (!node.WallRight && node.X + 1 < width)
            yield return maze[node.X + 1, node.Y];
    }
}
