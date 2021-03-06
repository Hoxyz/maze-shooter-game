using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamaging {
    private void OnCollisionEnter2D(Collision2D other) {
        List<IDamageable> damageables;
        InterfaceUtility.GetInterfaces<IDamageable>(out damageables, other.gameObject);
        if (damageables.Count > 0) {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(Damage);
        }
    }

    public float bulletDamage = 1f;
    public float Damage => bulletDamage;
}