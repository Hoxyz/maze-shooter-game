using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    private AStarGrid grid;
    private PathRequestManager requestManager;
    private void Awake() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<AStarGrid>();
    }

    private IEnumerator FindPath(Vector2 startPosition, Vector2 targetPosition) {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;
        
        AStarNode startNode = grid.NodeFromWorldPosition(startPosition);
        AStarNode targetNode = grid.NodeFromWorldPosition(targetPosition);

        if (!startNode.walkable || !targetNode.walkable) {
            yield break;
        }

        Heap<AStarNode> openSet = new Heap<AStarNode>(grid.MaxSize);
        HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            AStarNode currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode) {
                sw.Stop();
                print("Path found in " + sw.ElapsedMilliseconds);
                pathSuccess = true;
                
                break;
            }

            foreach (AStarNode neighbour in grid.GetNeighbours(currentNode)) {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) {
                        openSet.Add(neighbour);
                    }
                    else {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }

        if (pathSuccess) {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        yield return null;
    }

    private Vector2[] RetracePath(AStarNode startNode, AStarNode endNode) {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = endNode;
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        Vector2[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector2[] SimplifyPath(List<AStarNode> path) {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
            }

            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }

    private int GetDistance(AStarNode a, AStarNode b) {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);

        if (dstX > dstY) {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public void StartFindPath(Vector2 startPos, Vector2 targetPos) {
        StartCoroutine(FindPath(startPos, targetPos));
    }
    
}