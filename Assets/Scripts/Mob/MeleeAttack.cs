using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Mytona.MobCharacter {
    public class MeleeAttack : MobAttack, IMobComponent {
        protected override IEnumerator Attack() {
            Debug.Log("ATTACK");
            AttackIndicatorState(true);
            mobAnimator.StartAttackAnimation();
            mover.Active = false;
            yield return new WaitForSeconds(AttackDelay);
            AttackIndicatorState(false);
            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= DamageDistance) {
                Player.Instance.TakeDamage(mob.Damage);
            }

            mover.Active = true;
            attacking = false;
            _attackCoroutine = null;
        }
    }
}