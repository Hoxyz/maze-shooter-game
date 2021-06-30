using System.Collections.Generic;
using Random = UnityEngine.Random;

public class KruskalMazeGenerator : MazeGenerator{
    
    private int rows = 13, cols = 13;
    private DisjointSet ds = new DisjointSet();
    private bool[,] maze;
    private List<((int, int), (int, int))> edges;
    
    public override void setRows(int rows) {
        this.rows = rows;
    }

    public override void setCols(int cols) {
        this.cols = cols;
    }
    
    private void InitDisjointSet() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                ds.changeParent((i, j), (i, j));
                ds.changeSize((i, j), 1);
            }
        }
    }

    private void InitMaze() {
        maze = new bool[rows, cols];
        InitDisjointSet();
        edges = new List<((int, int), (int, int))>();
        
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (i == 0 || i == rows - 1 || j == 0 || j == cols - 1 || j % 2 == 0 || i % 2 == 0) {
                    maze[i, j] = true;
                }
                else {
                    maze[i, j] = false;
                }

                if (maze[i, j] && i > 0 && i < rows - 1 && j > 0 && j < cols - 1) {
                    if (!maze[i, j - 1] && !maze[i, j + 1]) {
                        edges.Add(((i, j - 1), (i, j + 1)));
                    }
                    if (!maze[i - 1, j] && !maze[i + 1, j]) {
                        edges.Add(((i - 1, j), (i + 1, j)));
                    }
                }
            }
        }
        
        for (int i = 0; i < edges.Count - 1; i++) {
            int r = Random.Range(i, edges.Count);
            ((int, int), (int, int)) tmp = edges[i];
            edges[i] = edges[r];
            edges[r] = tmp;
        }
    }
    
    public override bool[,] GenerateMaze() {
        InitMaze();
        for (int i = 0; i < edges.Count; i++) {
            (int, int) cell1 = edges[i].Item1;
            (int, int) cell2 = edges[i].Item2;
            (int, int) wallCell = ((cell1.Item1 + cell2.Item1) / 2, (cell1.Item2 + cell2.Item2) / 2);
            
            if (maze[cell1.Item1, cell1.Item2] || maze[cell2.Item1, cell2.Item2]) {
                continue;
            }
            
            if (!ds.Find(cell1, cell2)) {
                maze[wallCell.Item1, wallCell.Item2] = false;
                ds.WeightedUnion(cell1, cell2);
            }
        }

        return maze;
    }
}