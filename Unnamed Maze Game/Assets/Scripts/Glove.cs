using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour, IDamaging {
    public float Damage => 3f;

    public void Collision(Collision2D other) {
        List<IDamageable> damageables;
        InterfaceUtility.GetInterfaces<IDamageable>(out damageables, other.gameObject);
        if (damageables.Count > 0) {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(Damage);
        }
    }
}