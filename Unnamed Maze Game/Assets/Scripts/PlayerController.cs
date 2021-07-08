using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb2d;
    private float nextShootTime = 0f;
    private bool bulletToggle = false;
    [SerializeField] private float shootForce = 5f;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject bullet1Prefab;
    [SerializeField] private GameObject bullet2Prefab;
    [SerializeField] private float shootRate;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        RotateTowardsMouse();
        Shoot();
        if (Input.GetKeyDown(KeyCode.Space)) {
            bulletToggle = !bulletToggle;
        }
    }

    private void RotateTowardsMouse() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    private void Shoot() {
        if (Input.GetMouseButton(0) && Time.time > nextShootTime) {
            nextShootTime = Time.time + shootRate;
            rb2d.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Destroy(muzzleFlash, 0.1f);
            GameObject bullet = Instantiate(bulletToggle ? bullet2Prefab : bullet1Prefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0f, 0f, 180f));
            bullet.GetComponent<Rigidbody2D>().AddForce((Vector2) (-transform.up * bulletForce), ForceMode2D.Impulse);
            Destroy(bullet, 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Goal")) {
            GameEvents.getInstance().ReachedGoal();
        }
    }
}