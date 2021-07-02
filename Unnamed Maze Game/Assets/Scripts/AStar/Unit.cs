using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    private Transform target;
    [SerializeField] private float speed = 3f;
    private Vector2[] path;
    private int targetIndex;

    private void Update() {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null) {
            target = playerGameObject.GetComponent<Transform>();
        }
        if (target != null && transform.position != target.position) {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    private void OnPathFound(Vector2[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            StopCoroutine("FollowPath");
            path = newPath;
            targetIndex = 0;
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        if (path.Length < 1) yield break;
        Vector2 currentWaypoint = path[0];

        while (true) {
            if ((Vector2)transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);
                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            Destroy(this.gameObject);
            other.gameObject.tag = "DeadBullet";
        }
    }
}