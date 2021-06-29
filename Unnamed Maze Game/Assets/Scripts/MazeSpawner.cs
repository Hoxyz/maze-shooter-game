using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour {
    private MazeGenerator mazeGenerator;
    private bool[,] maze;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject playerPrefab;

    private void Start() {
        mazeGenerator = new KruskalMazeGenerator();
        SpawnMaze();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            foreach (GameObject wall in walls) {
                Destroy(wall);
            }
            Destroy(player);
            SpawnMaze();
        }
    }

    private void SpawnMaze() {
        maze = mazeGenerator.GenerateMaze();

        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);
        
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                Vector3 position = new Vector3(-rows / 2 + i, -cols / 2 + j, 0f);
                if (i == 1 && j == 1) {
                    Instantiate(playerPrefab, position, Quaternion.identity);
                }
                if (maze[i, j]) {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
            }
        }
    }
    
}