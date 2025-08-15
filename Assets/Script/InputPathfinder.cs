using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPathfinder : MonoBehaviour
{
    private Node[,] graph;
    private float cellSize;
    private Camera cameraMain;
    private Transform target;

    public event Action<List<Node>> OnFindedPath;

    private void Awake()
    {
        cameraMain = Camera.main;
    }

    public void Inject(Node[,] graph, float cellSize,Transform target)
    {
        this.graph = graph;
        this.cellSize = cellSize;
        this.target = target;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 clickPos = hit.point;
                Node start = GetClosestNode(this.target.position);
                Node target = GetClosestNode(clickPos);

                List<Node> path = Pathfinding.FindPath(graph,start, target);
                if(path != null) OnFindedPath?.Invoke(path);
            }
        }
    }
    
    private Node GetClosestNode(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cellSize);
        int y = Mathf.RoundToInt(worldPos.z / cellSize);
        x = Mathf.Clamp(x, 0, graph.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, graph.GetLength(1) - 1);
        return graph[x, y];
    }

}
