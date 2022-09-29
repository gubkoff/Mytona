using System;
using UnityEngine;

namespace Mytona.PlayerCharacter {
	public class PlayerAnimator : MonoBehaviour {
		[SerializeField] private Animator animator;

		private void Awake() {
			EventBus<PlayerInputMessage>.Sub(AnimatePlayer);
			EventBus.Sub(AnimateDeath, EventBus.PLAYER_DEATH);
		}

		private void AnimatePlayer(PlayerInputMessage message) {
			animator.SetBool("IsRun", message.MovementDirection.sqrMagnitude > 0);
		}

		private void OnDestroy() {
			EventBus<PlayerInputMessage>.Unsub(AnimatePlayer);
			EventBus.Unsub(AnimateDeath, EventBus.PLAYER_DEATH);
		}

		private void AnimateDeath() {
			animator.SetTrigger("Death");
		}

		public void TriggerShoot() {
			animator.SetTrigger("Shoot");
		}
	}
}