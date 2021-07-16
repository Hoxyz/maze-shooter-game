using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 pos = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }
}