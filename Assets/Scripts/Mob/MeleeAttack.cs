using System.Collections;
using Mytona.PlayerCharacter;
using Mytona.Utils;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class MeleeAttack : MobAttack, IMobComponent {
        protected override IEnumerator Attack() {
            AttackIndicatorState(true);
            mobAnimator.StartAttackAnimation();
            mover.Active = false;
            yield return new WaitForSeconds(attackDelay);
            AttackIndicatorState(false);
            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= damageDistance) {
                Player.Instance.TakeDamage(mob.Damage);
            }

            mover.Active = true;
            attacking = false;
            _attackCoroutine = null;
        }
    }
}