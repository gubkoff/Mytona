using System;
using System.Collections;
using System.Collections.Generic;
using Mytona.MobCharacter;
using Mytona.PlayerCharacter;
using UnityEngine;

namespace Mytona.Ammo {
    public class Rocket : Projectile {
        [SerializeField] private float damageRadius;
        [SerializeField] private GameObject explosion;

        protected override void OnTriggerEnter(Collider other) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
            foreach (var nearObject in colliders) {
                Rigidbody rb = nearObject.GetComponent<Rigidbody>();
                if (rb != null) {
                    if (damagePlayer && nearObject.CompareTag("Player")) {
                        nearObject.GetComponent<Player>().TakeDamage(Damage);
                    }

                    if (damageMob && nearObject.CompareTag("Mob")) {
                        var mob = nearObject.GetComponent<Mob>();
                        mob.TakeDamage(Damage);
                    }
                    // rb.AddExplosionForce(200f, transform.position, DamageRadius);
                }
            }

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
