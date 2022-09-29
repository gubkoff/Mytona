using System;
using System.Collections;
using System.Collections.Generic;
using Mytona.MobCharacter;
using UnityEngine;

public class Rocket : Projectile {
    [SerializeField] private float DamageRadius;
    [SerializeField] private GameObject Explosion;
    
    protected override void OnTriggerEnter(Collider other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, DamageRadius);
        foreach (var nearObject in colliders) {
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();
            if (rb != null) {
                if (DamagePlayer && nearObject.CompareTag("Player"))
                {
                    nearObject.GetComponent<Player>().TakeDamage(Damage);
                }
		
                if (DamageMob && nearObject.CompareTag("Mob"))
                {
                    var mob = nearObject.GetComponent<Mob>();
                    mob.TakeDamage(Damage);
                }
                // rb.AddExplosionForce(200f, transform.position, DamageRadius);
            }
        }

        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }
}
