using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;

    private static PathRequestManager instance;

    private Pathfinding pathfinding;

    private bool isProcessingPath;

    private void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    private void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector2[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest {
        public Vector2 pathStart, pathEnd;
        public Action<Vector2[], bool> callback;

        public PathRequest(Vector2 start, Vector2 end, Action<Vector2[], bool> callback) {
            pathStart = start;
            pathEnd = end;
            this.callback = callback;
        }
    }
    
}