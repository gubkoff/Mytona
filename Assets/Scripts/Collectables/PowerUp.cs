using System;
using Mytona.PlayerCharacter;
using UnityEngine;

namespace Mytona.Collectables {
	public class PowerUp : MonoBehaviour {
		[SerializeField] private int health;
		[SerializeField] private int damage;
		[SerializeField] private float moveSpeed;

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Player")) {
				other.GetComponent<Player>().Upgrade(health, damage, moveSpeed);
				Destroy(gameObject);
			}
		}
	}
}