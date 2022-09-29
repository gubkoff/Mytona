using System;
using System.Collections;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class Mob : MonoBehaviour {
        [SerializeField] private float damage = 1;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float health = 3;
        [SerializeField] private float maxHealth = 3;
        [SerializeField] private float timeExistAfterDeath = 2;

        public Action<float, float> OnHPChange = null;

        public float Damage => damage;
        public float MaxHealth => maxHealth;

        public void TakeDamage(float amount) {
            if (health <= 0)
                return;
            health -= amount;
            OnHPChange?.Invoke(health, -amount);
            if (health <= 0) {
                Death();
            }
        }

        public void Heal(float amount) {
            if (health <= 0)
                return;
            health += amount;
            if (health > maxHealth) {
                health = maxHealth;
            }

            OnHPChange?.Invoke(health, amount);
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
            yield return new WaitForSeconds(timeExistAfterDeath);
            Destroy(gameObject);
        }
    }
}