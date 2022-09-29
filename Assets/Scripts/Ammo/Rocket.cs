using System;
using System.Collections;
using System.Collections.Generic;
using Mytona.MobCharacter;
using UnityEngine;

public class Rocket : Projectile {
    [SerializeField] private float DamageRadius;
    
    protected override void OnTriggerEnter(Collider other) {
        Debug.Log("BOOM");
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);

        foreach (var nearObject in colliders) {
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(133f, transform.position, DamageRadius);
                if (destroyed)
                {
                    return;
                }
                if (DamagePlayer && other.CompareTag("Player"))
                {
                    other.GetComponent<Player>().TakeDamage(Damage);
                    destroyed = true;
                }
		
                if (DamageMob && other.CompareTag("Mob"))
                {
                    var mob = other.GetComponent<Mob>();
                    mob.TakeDamage(Damage);
                    destroyed = true;
                }
            }
        }
        Destroy(gameObject);
    }
}
