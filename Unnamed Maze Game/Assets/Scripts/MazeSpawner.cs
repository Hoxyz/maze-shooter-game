using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour {
    private MazeGenerator mazeGenerator;
    private bool[,] maze;
    [SerializeField] private GameObject wallPrefab;

    private void Start() {
        mazeGenerator = new KruskalMazeGenerator();
        maze = mazeGenerator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++) {
            for (int j = 0; j < maze.GetLength(1); j++) {
                if (maze[i, j]) {
                    Instantiate(wallPrefab, new Vector3(i, j, 0f), Quaternion.identity);
                }
            }
        }
    }
}