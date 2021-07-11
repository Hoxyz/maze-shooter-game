using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MazeSpawner : MonoBehaviour {
    private MazeGenerator mazeGenerator;
    private bool[,] maze;
    [SerializeField] private int rows, cols;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject mortarEnemyPrefab;

    private void Awake() {
        mazeGenerator = new KruskalMazeGenerator();
        mazeGenerator.setRows(rows);
        mazeGenerator.setCols(cols);
        SpawnMaze();
        GameEvents.getInstance().onReachedGoal += SpawnMaze;
    }

    private void Update() {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnMaze();
        }*/
    }

    private void SpawnMaze() {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject goal = GameObject.FindGameObjectWithTag("Goal");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject wall in walls) {
            Destroy(wall);
        }

        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }

        Destroy(player);
        Destroy(goal);
        maze = mazeGenerator.GenerateMaze();

        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                Vector3 position = new Vector3(-rows / 2 + i, -cols / 2 + j, 0f);
                if (i == 1 && j == 1) {
                    Instantiate(playerPrefab, position, Quaternion.identity);
                }

                if (i == rows - 2 && j == cols - 2) {
                    Instantiate(goalPrefab, position, Quaternion.identity);
                }

                if (maze[i, j]) {
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else {
                    if (Random.Range(0f, 1f) < 0.1f) {
                        if (Random.Range(0f, 1f) < 0.5f) {
                            Instantiate(enemyPrefab, position, Quaternion.identity);
                        }
                        else {
                            Instantiate(mortarEnemyPrefab, position, Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}