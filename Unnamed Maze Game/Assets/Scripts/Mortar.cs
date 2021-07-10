using UnityEngine;

public class Mortar : MonoBehaviour {
    [SerializeField] private GameObject mortarBulletPrefab;

    private void Update() {
        Shoot();
    }

    private void Shoot() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject mortarBullet = Instantiate(mortarBulletPrefab, transform.position, Quaternion.identity);
            mortarBullet.GetComponent<MortarBullet>().SetTargetLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}