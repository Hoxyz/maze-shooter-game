using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform target;
    
    void Update() {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null) {
            target = playerGameObject.GetComponent<Transform>();
        }
        if (target != null) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * (transform.position - target.position).magnitude * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}