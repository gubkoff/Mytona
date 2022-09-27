using System.Collections;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class RangeAttack : MobAttack, IMobComponent {
        [SerializeField] private float AttackCooldown = 2f;
        [SerializeField] private Projectile Bullet;

        protected override IEnumerator Attack() {
            AttackIndicatorState(true);
            mobAnimator.StartAttackAnimation();
            mover.Active = false;
            yield return new WaitForSeconds(AttackDelay);
            AttackIndicatorState(false);
            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= AttackDistance) {
                var bullet = Instantiate(Bullet, transform.position,
                    Quaternion.LookRotation((Player.Instance.transform.position - transform.position).Flat().normalized,
                        Vector3.up));
                bullet.SetDamage(mob.Damage);
            }

            mover.Active = true;
            yield return new WaitForSeconds(AttackCooldown);
            attacking = false;
            _attackCoroutine = null;
        }
    }
}