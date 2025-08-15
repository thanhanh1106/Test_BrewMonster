using System;
using UnityEngine;

// định add vcontainer nhưng mà thôi kệ
public class Container : MonoBehaviour
{
    public static Container  instance{get; private set;}
    
    [SerializeField] TopDownCamera _camera;
    [SerializeField] private MazeView mazeView;
    [SerializeField] private int widthMaze = 20;
    [SerializeField] private int heightMaze = 10;
    
    private PlayerController playerController;
    [SerializeField] private Agent agentPf;
    [SerializeField] private InputPathfinder  pathfinder;
    
    
    // presenter
    public MazePresenter MazePresenter {get; private set;}

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        BuildMaze();
        BuildPlayer();
    }

    private void BuildMaze()
    {
        var mazeGenerator = new EllersMazeGenerator();
        MazePresenter = new MazePresenter(mazeView, mazeGenerator, widthMaze, heightMaze);
        _camera.PositionCamera(widthMaze, heightMaze,mazeView.CellSize);
        
    }
    
    private void BuildPlayer()
    {
        var playerAgent = Instantiate(agentPf);
        playerAgent.SetCellSize(MazePresenter.CellSize);
        var startNode = MazePresenter.Maze[0, 0];
        playerAgent.transform.position = new Vector3(startNode.X,0.5f,startNode.Y); // 0.5f, do cái capsule nó bị chìm
        pathfinder.Inject(MazePresenter.Maze,MazePresenter.CellSize,playerAgent.transform);
        playerController = new PlayerController(playerAgent);
        pathfinder.OnFindedPath += playerController.Move;

    }
}
