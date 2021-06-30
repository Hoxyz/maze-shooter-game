using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour {
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    
    private AStarNode[,] grid;
    
    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    public bool displayGridGizmos = false;
    
    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }
    
    private void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update() {
        CreateGrid();
    }

    public List<AStarNode> GetNeighbours(AStarNode node) {
        List<AStarNode> neighbours = new List<AStarNode>();
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private void CreateGrid() {
        grid = new AStarNode[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y) -
                                  Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) +
                                     Vector2.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask);
                grid[x, y] = new AStarNode(walkable, worldPoint, x, y);
            }
        }
    }

    public AStarNode NodeFromWorldPosition(Vector2 worldPosition) {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        
        if (grid != null && displayGridGizmos) {
            foreach (AStarNode node in grid) {
                Gizmos.color = node.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector2.one * (nodeDiameter - 0.1f));
            }
        }
    }
}