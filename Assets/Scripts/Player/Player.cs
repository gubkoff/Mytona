using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytona.PlayerCharacter {
    public class Player : MonoBehaviour {
        public static Player Instance;
        [SerializeField] private float damage = 1;
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float health = 3;
        [SerializeField] private float maxHealth = 3;

        public Action<WeaponType> OnWeaponChange = null;
        public Action<float, float> OnHPChange = null;
        public Action OnUpgrade = null;

        private WeaponType weaponType = WeaponType.None;

        private void Awake() {
            if (Instance != null) {
                DestroyImmediate(gameObject);
            }
            else {
                Instance = this;
            }
        }

        public float GetDamage() {
            return damage;
        }

        public float GetMoveSpeed() {
            return moveSpeed;
        }

        public float GetMaxHealth() {
            return maxHealth;
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        public void TakeDamage(float amount) {
            if (health <= 0)
                return;
            health -= amount;
            if (health <= 0) {
                EventBus.Pub(EventBus.PLAYER_DEATH);
            }

            OnHPChange?.Invoke(health, -amount);
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


        public void Upgrade(float hp, float dmg, float ms) {
            damage += dmg;
            health += hp;
            maxHealth += hp;
            moveSpeed += ms;
            OnUpgrade?.Invoke();
            OnHPChange?.Invoke(health, hp);
        }

        public void ChangeWeapon(WeaponType type) {
            weaponType = type;
            OnWeaponChange?.Invoke(type);
        }

        public WeaponType GetCurrentWeapon() {
            if (weaponType == WeaponType.None) {
                return WeaponType.Pistol;
            }

            return weaponType;
        }
    }
}