using System.Collections;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class RangeAttack : MobAttack, IMobComponent {
        [SerializeField] private float AttackCooldown = 2f;
        [SerializeField] private Projectile Bullet;
        [SerializeField] private Transform BulletSpawn;

        protected override IEnumerator Attack() {
            AttackIndicatorState(true);
            yield return new WaitForSeconds(AttackDelay);
            mover.Active = false;
            mobAnimator.StartAttackAnimation();
            AttackIndicatorState(false);
            yield return new WaitForSeconds(AttackCooldown);
            mover.Active = true;
            attacking = false;
            _attackCoroutine = null;
        }

        public override void Shoot() {
            var bullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
            bullet.SetDamage(mob.Damage);
        }
    }
}