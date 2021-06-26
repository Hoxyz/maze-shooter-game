using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown2DCharacterMovement : MonoBehaviour {
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private KeyCode up = KeyCode.W;
    [SerializeField] private KeyCode down = KeyCode.S;
    [SerializeField] private KeyCode left = KeyCode.A;
    [SerializeField] private KeyCode right = KeyCode.D;

    private float horizontal = 0f;
    private float vertical = 0f;
    private Rigidbody2D rb2d;
    
    private void Awake() {
        if (GetComponent<Rigidbody2D>() == null) {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        horizontal = vertical = 0f;
        if (Input.GetKey(up)) {
            vertical += 1f;
        }
        if (Input.GetKey(down)) {
            vertical += -1f;
        }
        if (Input.GetKey(right)) {
            horizontal += 1f;
        }
        if (Input.GetKey(left)) {
            horizontal += -1f;
        }
    }

    private void FixedUpdate() {
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        rb2d.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
