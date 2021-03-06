using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGenerator {
    public abstract bool[,] GenerateMaze();
    public abstract void setRows(int rows);
    public abstract void setCols(int cols);
}