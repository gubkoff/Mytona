using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class Mob : MonoBehaviour {
        //TODO change
        public float Damage = 1;
        [SerializeField] private float MoveSpeed = 3.5f;

        [SerializeField] private float Health = 3;
        
        [SerializeField] private float TimeExistAfterDeath = 2;

        //TODO change
        public float MaxHealth = 3;

        public Action<float, float> OnHPChange = null;

        public void TakeDamage(float amount) {
            if (Health <= 0)
                return;
            Health -= amount;
            OnHPChange?.Invoke(Health, -amount);
            if (Health <= 0) {
                Death();
            }
        }

        public void Heal(float amount) {
            if (Health <= 0)
                return;
            Health += amount;
            if (Health > MaxHealth) {
                Health = MaxHealth;
            }

            OnHPChange?.Invoke(Health, amount);
        }

        public void Death() {
            EventBus.Pub(EventBus.MOB_KILLED);
            var components = GetComponents<IMobComponent>();
            foreach (var component in components) {
                component.OnDeath();
            }

            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(ClearAfterDeath());
        }

        private IEnumerator ClearAfterDeath() {
            yield return new WaitForSeconds(TimeExistAfterDeath);
            Destroy(gameObject);
        }
    }
}