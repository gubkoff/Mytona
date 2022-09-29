using System.Collections;
using Mytona.PlayerCharacter;
using Mytona.Utils;
using UnityEngine;

namespace Mytona.MobCharacter {
    [RequireComponent(typeof(MobMover))]
    [RequireComponent(typeof(Mob))]
    public abstract class MobAttack : MonoBehaviour {
        [SerializeField] protected float attackDistance = 1f;
        [SerializeField] protected float damageDistance = 1f;
        [SerializeField] protected float attackDelay = 1f;
        [SerializeField] protected GameObject attackIndicator;

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
            EventBus.Sub(Shoot, EventBus.MOB_SHOOT);
            AttackIndicatorState(false);
        }

        protected virtual void Update() {
            if (attacking) {
                return;
            }

            var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
            if (playerDistance <= attackDistance) {
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
            if (attackIndicator != null) {
                attackIndicator.SetActive(state);
            }
        }

        protected void LateUpdate() {
            if (attackIndicator != null) {
                attackIndicator.transform.rotation = Camera.main.transform.rotation;
            }
        }
        
        public virtual void Shoot() {
            
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