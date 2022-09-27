using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytona.MobCharacter {
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    public abstract class MobAttack : MonoBehaviour {
        [SerializeField] protected float AttackDistance = 1f;
        [SerializeField] protected float DamageDistance = 1f;
        [SerializeField] protected float AttackDelay = 1f;
        [SerializeField] protected GameObject AttackIndicator;

        protected MobMover mover;
        protected Mob mob;
        protected MobAnimator mobAnimator;
        protected bool attacking = false;
        protected Coroutine _attackCoroutine = null;

        protected virtual void Awake() {
            mob = GetComponent<Mob>();
            mover = GetComponent<MobMover>();
            mobAnimator = GetComponent<MobAnimator>();
            EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
            AttackIndicatorState(false);
        }

        protected virtual void Update() {
            if (attacking) {
                return;
            }

            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= AttackDistance) {
                attacking = true;
                _attackCoroutine = StartCoroutine(Attack());
            }
        }

        protected virtual void OnDestroy() {
            EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
        }

        protected virtual IEnumerator Attack() {
            yield break;
        }

        protected void AttackIndicatorState(bool state) {
            if (AttackIndicator != null) {
                AttackIndicator.SetActive(state);
            }
        }

        protected void LateUpdate() {
            if (AttackIndicator != null) {
                AttackIndicator.transform.rotation = Camera.main.transform.rotation;
            }
        }

        public void OnDeath() {
            enabled = false;
            AttackIndicatorState(false);
            if (_attackCoroutine != null) {
                StopCoroutine(_attackCoroutine);
            }
        }
    }
}