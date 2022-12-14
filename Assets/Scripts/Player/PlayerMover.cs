using System;
using UnityEngine;

namespace Mytona.PlayerCharacter {
	public class PlayerMover : MonoBehaviour {
		private Player player;
		
		private void Awake() {
			player = GetComponent<Player>();
			EventBus<PlayerInputMessage>.Sub(MovePlayer);
		}

		private void MovePlayer(PlayerInputMessage message) {
			var speed = player.MoveSpeed;
			var delta = new Vector3(speed * message.MovementDirection.x, 0, speed * message.MovementDirection.y) *
			            Time.deltaTime;
			transform.position += delta;
			transform.forward = new Vector3(message.AimDirection.x, 0, message.AimDirection.y);
		}

		private void OnDestroy() {
			EventBus<PlayerInputMessage>.Unsub(MovePlayer);
		}
	}
}