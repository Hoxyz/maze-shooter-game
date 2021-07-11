using UnityEngine;

public class Mortar : MonoBehaviour, IDamageable {
    [SerializeField] private GameObject mortarBulletPrefab;
    [SerializeField] private float shotCD = 1f;
    private float nextShotTime = 0f;
    private Transform target;
    private float currentHealth = 2f;
    
    private void Update() {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject != null) {
            target = playerGameObject.GetComponent<Transform>();
        }
        if (target != null) {
            Shoot(target.position);
        }
    }

    private void Shoot(Vector2 targetPos) {
        if (Time.time > nextShotTime) {
            nextShotTime = Time.time + shotCD;
            
            GameObject mortarBullet = Instantiate(mortarBulletPrefab, transform.position, Quaternion.identity);
            mortarBullet.GetComponent<MortarBullet>().SetTargetLocation(targetPos);
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0f) {
            Destroy(gameObject);
        }
    }
}