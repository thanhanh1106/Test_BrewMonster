using System.Collections.Generic;

public class Node
{
    public int X {private set; get;}
    public int Y {private set; get;}
    
    public bool WallUp;
    public bool WallRight;
    public bool WallDown;
    public bool WallLeft;
    
    public float HCost;
    public float GCost;
    public float FCost => HCost + GCost;
    public Node CameFrom;
    
    public HashSet<Node> Neighbors {get; private set;}

    public Node(int x, int y)
    {
        Neighbors = new();
        X = x;
        Y = y;
        
        // node mới tạo ra đều có tường bao quanh
        WallUp = true;
        WallRight = true;
        WallDown = true;
        WallLeft = true;
        
    }

    public void AddNeighbor(Node neighbor) => Neighbors.Add(neighbor);
}
