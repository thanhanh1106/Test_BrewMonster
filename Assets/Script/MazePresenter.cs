
public class MazePresenter  
{
    private MazeView _view;
    private IMazeGenerator _generator;
    
    private Node[,] _maze;

    public Node[,] Maze => _maze;
    public float CellSize => _view.CellSize;
    public int MazeWidth => _maze.GetLength(0);
    public int MazeHeight => _maze.GetLength(1);
    
    public MazePresenter(MazeView  view, IMazeGenerator generator,int  width, int height)
    {
        _view = view;
        _generator = generator;
        CreateMaze(width, height);
    }

    public void CreateMaze(int width, int height)
    {
        _maze = _generator.Generate(width, height);
        _view.BuildMaze(_maze);
    }
}
