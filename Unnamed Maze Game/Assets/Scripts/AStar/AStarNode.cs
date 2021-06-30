using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : IHeapItem<AStarNode> {
    public bool walkable;
    public Vector3 worldPosition;
    public AStarNode parent;
    public int gridX;
    public int gridY;
    
    public int gCost;
    public int hCost;

    public int heapIndex;

    public AStarNode(bool walkable, Vector3 worldPosition, int gridX, int gridY) {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost => gCost + hCost;

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(AStarNode nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}