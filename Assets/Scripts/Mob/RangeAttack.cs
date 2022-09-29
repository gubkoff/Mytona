using System.Collections;
using Mytona.Ammo;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class RangeAttack : MobAttack, IMobComponent {
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private Projectile bullet;
        [SerializeField] private Transform bulletSpawn;

        protected override IEnumerator Attack() {
            AttackIndicatorState(true);
            yield return new WaitForSeconds(attackDelay);
            mover.Active = false;
            mobAnimator.StartAttackAnimation();
            AttackIndicatorState(false);
            yield return new WaitForSeconds(attackCooldown);
            mover.Active = true;
            attacking = false;
            _attackCoroutine = null;
        }

        public override void Shoot() {
            var bulletObj = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            bulletObj.Damage = mob.Damage;
        }
    }
}